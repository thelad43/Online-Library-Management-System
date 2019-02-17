namespace OnlineLibraryManagementSystem.Services
{
    using Models.Books;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<int> GetBooksCountAsync();

        Task<int> GetBorrowedBooksCountAsync();

        Task<IEnumerable<ShortBookServiceModel>> GetAllAsync(int page);

        Task AddAsync(string title, string description, string id);

        Task<BookServiceModel> ByIdAsync(int id);

        Task<IEnumerable<BookServiceModel>> ByAuthorAsync(string id, int page);

        Task<int> GetBooksByAuthorCountAsync(string id);
    }
}