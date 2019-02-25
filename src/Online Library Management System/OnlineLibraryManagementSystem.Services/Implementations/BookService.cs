namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Common.Mapping;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Books;
    using OnlineLibraryManagementSystem.Models;
    using System;
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

        public async Task AddAsync(string title, string description, string id)
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

        public async Task<string> BorrowAsync(int id, string userName)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            var book = await this.db.Books.FindAsync(id);

            if (user == null || book == null)
            {
                throw new InvalidOperationException();
            }

            // This book is already borrowed
            if (book.BorrowerId != null)
            {
                throw new InvalidOperationException();
            }

            book.BorrowerId = user.Id;
            book.BorrowedTimes++;

            await this.db.SaveChangesAsync();

            return book.Title;
        }

        public async Task<IEnumerable<BorrowedBookServiceModel>> BorrowedAsync(int page)
            => await this.db
                .Books
                .Where(b => b.BorrowerId != null)
                .OrderBy(b => b.BorrowedTimes)
                .Skip((page - 1) * BooksOnPage)
                .Take(BooksOnPage)
                .To<BorrowedBookServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<BookServiceModel>> ByAuthorAsync(string id, int page)
            => await this.db
                .Books
                .Where(b => b.AuthorId == id)
                .OrderBy(b => b.BorrowedTimes)
                .Skip((page - 1) * BooksOnPage)
                .Take(BooksOnPage)
                .To<BookServiceModel>()
                .ToListAsync();

        public async Task<BookServiceModel> ByIdAsync(int id)
            => await this.db
                .Books
                .Where(b => b.Id == id)
                .To<BookServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<string> DeleteAsync(int id)
        {
            var book = await this.db.Books.FindAsync(id);

            if (book == null)
            {
                throw new InvalidOperationException();
            }

            this.db.Books.Remove(book);

            await this.db.SaveChangesAsync();

            return book.Title;
        }

        public async Task<string> EditAsync(int id, string title, string description)
        {
            var book = await this.db.Books.FindAsync(id);

            if (book == null)
            {
                throw new InvalidOperationException();
            }

            book.Title = title;
            book.Description = description;

            await this.db.SaveChangesAsync();

            return book.Title;
        }

        public async Task<IEnumerable<BookDetailsServiceModel>> GetAllAsync(int page)
            => await this.db
                .Books
                .OrderBy(b => b.Id)
                .Skip((page - 1) * BooksOnPage)
                .Take(BooksOnPage)
                .To<BookDetailsServiceModel>()
                .ToListAsync();

        public async Task<int> GetByAuthorCountAsync(string id)
            => await this.db
                .Books
                .Where(b => b.AuthorId == id)
                .CountAsync();

        public async Task<int> GetBooksCountAsync()
            => await this.db
                .Books
                .CountAsync();

        public async Task<int> GetBorrowedCountAsync()
            => await this.db
                .Books
                .Where(b => b.BorrowerId != null)
                .CountAsync();

        public async Task<int> GetMyBorrowedCountAsync(string userName)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            var count = await this.db
                .Books
                .Where(b => b.BorrowerId == user.Id)
                .CountAsync();

            return count;
        }

        public async Task<IEnumerable<BorrowedBookServiceModel>> MyBorrowedAsync(string userName, int page)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            var books = await this.db
                .Books
                .Where(b => b.BorrowerId == user.Id)
                .OrderBy(b => b.BorrowedTimes)
                .Skip((page - 1) * BooksOnPage)
                .Take(BooksOnPage)
                .To<BorrowedBookServiceModel>()
                .ToListAsync();

            return books;
        }

        public async Task<string> ReturnAsync(int id, string userName)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            var book = await this.db.Books.FindAsync(id);

            if (user == null || book == null)
            {
                throw new InvalidOperationException();
            }

            book.BorrowerId = null;

            await this.db.SaveChangesAsync();

            return book.Title;
        }
    }
}