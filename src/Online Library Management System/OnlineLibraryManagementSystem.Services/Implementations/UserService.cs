namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Common.Mapping;
    using Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using OnlineLibraryManagementSystem.Common;
    using OnlineLibraryManagementSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly OnlineLibraryManagementSystemDbContext db;
        private readonly UserManager<User> userManager;

        public UserService(OnlineLibraryManagementSystemDbContext db,
            UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task<string> AddAuthor(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException();
            }

            await this.userManager.AddToRoleAsync(user, GlobalConstants.AuthorRole);

            return user.UserName;
        }

        public int GetAuthorsCount()
            => this.db.Users.Where(u => u.AuthorBooks.Any()).Count();

        public async Task<IEnumerable<AuthorServiceModel>> GetUsers()
            => await this.db.Users.Where(u => !u.AuthorBooks.Any()).To<AuthorServiceModel>().ToListAsync();
    }
}