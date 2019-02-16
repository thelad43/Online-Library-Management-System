namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Common.Mapping;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Books;
    using OnlineLibraryManagementSystem.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.GlobalConstants;

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

        public async Task<IEnumerable<ShortBookServiceModel>> GetAllBooks(int page)
            => await this.db
                .Books
                .OrderBy(b => b.Id)
                .Skip((page - 1) * BooksOnPage)
                .Take(BooksOnPage)
                .To<ShortBookServiceModel>()
                .ToListAsync();

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