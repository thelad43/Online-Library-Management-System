namespace OnlineLibraryManagementSystem.Services.Models.Books
{
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Models;

    public class BorrowedBookServiceModel : IMapFrom<Book>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}