namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Common;
    using Common.Mapping;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using OnlineLibraryManagementSystem.Models;
    using Services.Models.Authors;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

            await this.userManager.AddToRoleAsync(user, GlobalConstants.AuthorRole);

            return user.UserName;
        }

        public async Task<int> GetAuthorsCountAsync()
            => await this.db
                .Users
                .Where(u => u.AuthorBooks.Any())
                .CountAsync();

        public async Task<IEnumerable<AuthorServiceModel>> GetUsersAsync()
            => await this.db
                .Users
                .Where(u => !u.AuthorBooks.Any())
                .To<AuthorServiceModel>()
                .ToListAsync();
    }
}