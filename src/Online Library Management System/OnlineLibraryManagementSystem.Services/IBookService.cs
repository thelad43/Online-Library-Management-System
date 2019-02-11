namespace OnlineLibraryManagementSystem.Services
{
    using Models;
    using System.Threading.Tasks;

    public interface IBookService
    {
        int GetBooksCount();

        int GetBorrowedBooksCount();

        Task Add(string title, string description, string id);

        Task<BookServiceModel> ById(int id);
    }
}