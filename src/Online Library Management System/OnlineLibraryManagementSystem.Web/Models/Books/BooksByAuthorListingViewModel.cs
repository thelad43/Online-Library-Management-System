namespace OnlineLibraryManagementSystem.Web.Models.Books
{
    using Services.Models.Books;
    using System.Collections.Generic;

    public class BooksByAuthorListingViewModel
    {
        public string Author { get; set; }

        public PageViewModel Page { get; set; }

        public IEnumerable<BookServiceModel> Books { get; set; }
    }
}