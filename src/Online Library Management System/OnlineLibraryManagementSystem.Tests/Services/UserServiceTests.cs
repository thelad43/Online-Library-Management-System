namespace OnlineLibraryManagementSystem.Tests.Services
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using OnlineLibraryManagementSystem.Common;
    using OnlineLibraryManagementSystem.Models;
    using OnlineLibraryManagementSystem.Services.Implementations;
    using OnlineLibraryManagementSystem.Services.Models.Authors;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class UserServiceTests
    {
        public UserServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task GetAuthorsCountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var i = 0; i < 100; i++)
            {
                var user = new User
                {
                    UserName = $"Username {i}"
                };

                await db.AddAsync(user);

                if (i % 10 == 0)
                {
                    await db.SaveChangesAsync();

                    await db.AddAsync(new Book
                    {
                        Title = $"Book Title {i + 1000}",
                        AuthorId = user.Id
                    });
                }
            }

            await db.SaveChangesAsync();

            var userSerivce = new UserService(db, this.GetUserManagerMock().Object);

            var authorsCount = await userSerivce.GetAuthorsCountAsync();

            authorsCount.Should().Be(10);
        }

        [Fact]
        public async Task GetNonAuthorsAsyncShouldReturnOnlyNonAuthors()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var i = 0; i < 200; i++)
            {
                var user = new User
                {
                    UserName = $"Username {i}"
                };

                await db.AddAsync(user);

                if (i % 10 == 0)
                {
                    await db.SaveChangesAsync();

                    await db.AddAsync(new Book
                    {
                        Title = $"Book Title {i + 1000}",
                        AuthorId = user.Id
                    });
                }
            }

            await db.SaveChangesAsync();

            var userSerivce = new UserService(db, this.GetUserManagerMock().Object);

            var nonAuthors = await userSerivce.GetNonAuthorsAsync();

            nonAuthors
                .Should()
                .HaveCount(180);

            foreach (var nonAuthor in nonAuthors)
            {
                nonAuthor
                    .Should()
                    .Match<AuthorServiceModel>(a => a.BooksCount == 0);
            }
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnUsersByPage()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var i = 0; i < 45; i++)
            {
                var user = new User
                {
                    UserName = $"Username {i}"
                };

                await db.AddAsync(user);
            }

            await db.SaveChangesAsync();

            var userSerivce = new UserService(db, this.GetUserManagerMock().Object);

            for (var i = 1; i < 3; i++)
            {
                var users = await userSerivce.GetAllAsync(i);

                users
                    .Should()
                    .BeInAscendingOrder(u => u.UserName);

                users
                    .Should()
                    .HaveCount(GlobalConstants.UsersCountOnPage);
            }

            var nonExistingUsers = await userSerivce.GetAllAsync(4);

            nonExistingUsers
                .Should()
                .BeEmpty();
        }

        [Fact]
        public async Task GetUsersCountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            const int UsersCount = 45;

            for (var i = 0; i < UsersCount; i++)
            {
                var user = new User
                {
                    UserName = $"Some Username {i}"
                };

                await db.AddAsync(user);
            }

            await db.SaveChangesAsync();

            var userSerivce = new UserService(db, this.GetUserManagerMock().Object);

            var count = await userSerivce.GetUsersCountAsync();

            count.Should().Be(UsersCount);
        }

        [Fact]
        public async Task SearchAsyncShouldReturnCorrectResults()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var i = 0; i < 45; i++)
            {
                var user = new User
                {
                    UserName = $"Some User {i}"
                };

                await db.AddAsync(user);
            }

            await db.SaveChangesAsync();

            var userSerivce = new UserService(db, this.GetUserManagerMock().Object);

            const string UserSearchText = "user";

            for (var i = 0; i < 3; i++)
            {
                var searchResults = await userSerivce.SearchAsync(1, UserSearchText);

                searchResults
                    .Should()
                    .HaveCount(GlobalConstants.UsersCountOnPage);

                foreach (var searchResult in searchResults)
                {
                    searchResult
                        .Should()
                        .Match<AuthorServiceModel>(a => a.Name.ToLower().Contains(UserSearchText));
                }
            }
        }

        [Fact]
        public async Task GetCountBySearchAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            const int UsersCount = 45;

            for (var i = 0; i < UsersCount; i++)
            {
                var user = new User
                {
                    UserName = $"Some User {i}"
                };

                await db.AddAsync(user);
            }

            await db.SaveChangesAsync();

            var userSerivce = new UserService(db, this.GetUserManagerMock().Object);

            const string UserSearchText = "user";

            var count = await userSerivce.GetCountBySearchAsync(UserSearchText);

            count.Should().Be(UsersCount);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
            => new Mock<UserManager<User>>(
                   new Mock<IUserStore<User>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<User>>().Object,
                   new IUserValidator<User>[0],
                   new IPasswordValidator<User>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<User>>>().Object);
    }
}