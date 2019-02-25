namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Common.Mapping;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models.Admin;
    using Models.Authors;
    using OnlineLibraryManagementSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.GlobalConstants;

    public class UserService : IUserService
    {
        private readonly OnlineLibraryManagementSystemDbContext db;
        private readonly UserManager<User> userManager;

        public UserService(
            OnlineLibraryManagementSystemDbContext db,
            UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<string> AddAuthorAsync(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException();
            }

            await this.userManager.AddToRoleAsync(user, AuthorRole);

            return user.UserName;
        }

        public async Task<int> GetAuthorsCountAsync()
            => await this.db
                .Users
                .Where(u => u.AuthorBooks.Any())
                .CountAsync();

        public async Task<IEnumerable<AuthorServiceModel>> GetNonAuthorsAsync()
            => await this.db
                .Users
                .Where(u => !u.AuthorBooks.Any())
                .To<AuthorServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<UserAdminModel>> GetAsync(int page)
            => await this.db
                .Users
                .OrderBy(u => u.UserName)
                .Skip((page - 1) * UsersCountOnPage)
                .Take(UsersCountOnPage)
                .To<UserAdminModel>()
                .ToListAsync();

        public async Task<int> GetUsersCountAsync()
            => await this.db
                .Users
                .CountAsync();

        public async Task<IEnumerable<UserAdminModel>> SetRoleToModelAsync(IEnumerable<UserAdminModel> users)
        {
            var usersList = users.ToList();

            for (var i = 0; i < usersList.Count; i++)
            {
                var currentUserModel = usersList[i];
                var userName = currentUserModel.UserName;

                var user = await this.userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    throw new InvalidOperationException();
                }

                var roles = await this.userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    currentUserModel.Role += role;
                }

                if (currentUserModel.Role == null)
                {
                    currentUserModel.Role = "User";
                }
            }

            return usersList;
        }
    }
}