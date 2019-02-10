namespace OnlineLibraryManagementSystem.Services.Implementations
{
    using Data;
    using System.Linq;

    public class BookService : IBookService
    {
        private readonly OnlineLibraryManagementSystemDbContext db;

        public BookService(OnlineLibraryManagementSystemDbContext db)
        {
            this.db = db;
        }

        public int GetBooksCount()
            => this.db.Books.Count();

        public int GetBorrowedBooksCount()
            => this.db.Books.Where(b => b.BorrowerId != null).Count();
    }
}