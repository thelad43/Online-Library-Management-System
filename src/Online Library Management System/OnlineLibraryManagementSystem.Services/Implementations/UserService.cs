namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly OnlineLibraryManagementSystemDbContext db;

        public UserService(OnlineLibraryManagementSystemDbContext db)
        {
            this.db = db;
        }

        public int GetAuthorsCount()
            => this.db.Users.Where(u => u.AuthorBooks.Any()).Count();
    }
}