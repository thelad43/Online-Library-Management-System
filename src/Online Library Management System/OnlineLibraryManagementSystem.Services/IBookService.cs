namespace OnlineLibraryManagementSystem.Services
{
    public interface IBookService
    {
        int GetBooksCount();

        int GetBorrowedBooksCount();
    }
}