namespace OnlineLibraryManagementSystem.Services
{
    using Models.Books;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<int> GetBooksCountAsync();

        Task<int> GetBorrowedBooksCountAsync();

        Task<IEnumerable<BookDetailsServiceModel>> GetAllAsync(int page);

        Task AddAsync(string title, string description, string id);

        Task<BookServiceModel> ByIdAsync(int id);

        Task<IEnumerable<BookServiceModel>> ByAuthorAsync(string id, int page);

        Task<int> GetBooksByAuthorCountAsync(string id);

        Task<string> BorrowAsync(int id, string userName);

        Task<IEnumerable<BorrowedBookServiceModel>> MyBorrowedAsync(string userName, int page);

        Task<int> GetMyBorrowedBooksCountAsync(string userName);

        Task<string> ReturnAsync(int id, string userName);

        Task<IEnumerable<BorrowedBookServiceModel>> BorrowedAsync(int page);

        Task<string> EditAsync(int id, string title, string description);

        Task<string> DeleteAsync(int id);
    }
}