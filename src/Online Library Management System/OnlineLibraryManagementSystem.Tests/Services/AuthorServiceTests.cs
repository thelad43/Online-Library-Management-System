namespace OnlineLibraryManagementSystem.Tests.Services
{
    using Common;
    using FluentAssertions;
    using Models;
    using OnlineLibraryManagementSystem.Services.Implementations;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class AuthorServiceTests
    {
        private readonly Random randomGenerator;

        public AuthorServiceTests()
        {
            this.randomGenerator = new Random();
            Tests.Initialize();
        }

        [Fact]
        public async Task GetAllAsyncShouldReturnOnlyAuthorsWhoHaveBooks()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var userIndex = 0; userIndex < 60; userIndex++)
            {
                var user = new User
                {
                    Email = $"myEmail@sth.com{userIndex}",
                    UserName = $"Some Random username {userIndex}",
                };

                await db.Users.AddAsync(user);

                for (var bookIndex = 0; bookIndex < this.randomGenerator.Next(1, 11); bookIndex++)
                {
                    var book = new Book
                    {
                        AuthorId = user.Id,
                        Title = $"Some Title {bookIndex}",
                    };

                    await db.AddAsync(book);
                }

                await db.SaveChangesAsync();
            }

            var authorService = new AuthorService(db);

            var authorsPage1 = await authorService.GetAllAsync(1);

            authorsPage1
                .Should()
                .HaveCount(GlobalConstants.AuthorsOnPage);

            authorsPage1
                .Should()
                .BeInAscendingOrder(x => x.Name);

            var authorsPage2 = await authorService.GetAllAsync(2);

            authorsPage2
                .Should()
                .HaveCount(GlobalConstants.AuthorsOnPage);

            authorsPage2
                .Should()
                .BeInAscendingOrder(x => x.Name);

            var authorsPage3 = await authorService.GetAllAsync(3);

            authorsPage3
                .Should()
                .HaveCount(GlobalConstants.AuthorsOnPage);

            authorsPage3
                .Should()
                .BeInAscendingOrder(x => x.Name);

            var authorsPage4 = await authorService.GetAllAsync(4);

            authorsPage4
                .Should()
                .HaveCount(0);
        }

        [Fact]
        public async Task GetAuthorsCountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var i = 0; i < 20; i++)
            {
                var user = new User
                {
                    Email = $"myRandomEmail@sth.com{i}",
                    UserName = $"Some Random Second username {i}",
                };

                await db.Users.AddAsync(user);
            }

            for (var userIndex = 0; userIndex < 60; userIndex++)
            {
                var user = new User
                {
                    Email = $"myEmail@sth.com{userIndex}",
                    UserName = $"Some Random username {userIndex}",
                };

                await db.Users.AddAsync(user);

                for (var bookIndex = 0; bookIndex < this.randomGenerator.Next(1, 11); bookIndex++)
                {
                    var book = new Book
                    {
                        AuthorId = user.Id,
                        Title = $"Some Title {bookIndex}",
                    };

                    await db.AddAsync(book);
                }
            }

            for (var i = 0; i < 15; i++)
            {
                var user = new User
                {
                    Email = $"myRandom Third Email@sth.com{i}",
                    UserName = $"Some Random Third username {i}",
                };

                await db.Users.AddAsync(user);
            }

            await db.SaveChangesAsync();

            var authorService = new AuthorService(db);

            var authors = await authorService.GetAuthorsCountAsync();

            authors.Should().Be(60);
        }
    }
}