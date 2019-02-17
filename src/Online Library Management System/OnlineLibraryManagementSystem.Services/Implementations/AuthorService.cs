namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Common.Mapping;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Authors;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.GlobalConstants;

    public class AuthorService : IAuthorService
    {
        private readonly OnlineLibraryManagementSystemDbContext db;

        public AuthorService(OnlineLibraryManagementSystemDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AuthorServiceModel>> GetAllAsync(int page)
            => await this.db
                .Users
                .Where(u => u.AuthorBooks.Any())
                .OrderBy(u => u.UserName)
                .Skip((page - 1) * AuthorsOnPage)
                .Take(AuthorsOnPage)
                .To<AuthorServiceModel>()
                .ToListAsync();

        public async Task<int> GetAuthorsCountAsync()
            => await this.db
                .Users
                .Where(u => u.AuthorBooks.Any())
                .CountAsync();
    }
}