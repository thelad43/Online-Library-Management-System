namespace OnlineLibraryManagementSystem.Services
{
    using Models.Books;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        int GetBooksCount();

        int GetBorrowedBooksCount();

        Task<IEnumerable<ShortBookServiceModel>> GetAllBooks(int page);

        Task Add(string title, string description, string id);

        Task<BookServiceModel> ById(int id);
    }
}