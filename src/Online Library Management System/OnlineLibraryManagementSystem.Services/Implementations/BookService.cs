namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using OnlineLibraryManagementSystem.Common.Mapping;
    using OnlineLibraryManagementSystem.Models;
    using OnlineLibraryManagementSystem.Services.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class BookService : IBookService
    {
        private readonly OnlineLibraryManagementSystemDbContext db;

        public BookService(OnlineLibraryManagementSystemDbContext db)
        {
            this.db = db;
        }

        public async Task Add(string title, string description, string id)
        {
            var book = new Book
            {
                Title = title,
                Description = description,
                AuthorId = id
            };

            await this.db.AddAsync(book);
            await this.db.SaveChangesAsync();
        }

        public async Task<BookServiceModel> ById(int id)
            => await this.db
                .Books
                .Where(b => b.Id == id)
                .To<BookServiceModel>()
                .FirstOrDefaultAsync();

        public int GetBooksCount()
            => this.db
                .Books
                .Count();

        public int GetBorrowedBooksCount()
            => this.db
                .Books
                .Where(b => b.BorrowerId != null)
                .Count();
    }
}