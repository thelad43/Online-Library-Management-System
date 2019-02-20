namespace OnlineLibraryManagementSystem.Web.Models.Books
{
    using Services.Models.Books;
    using System.Collections.Generic;

    public class BorrowedBooksListingViewModel
    {
        public PageViewModel Page { get; set; }

        public IEnumerable<BorrowedBookServiceModel> Books { get; set; }
    }
}