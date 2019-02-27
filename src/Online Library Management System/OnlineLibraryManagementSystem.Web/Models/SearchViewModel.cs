namespace OnlineLibraryManagementSystem.Web.Models
{
    using Services.Models.Authors;
    using Services.Models.Books;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SearchViewModel
    {
        public string SearchText { get; set; }

        [Display(Name = "Search for books")]
        public bool SearchForBooks { get; set; }

        [Display(Name = "Search for authors")]
        public bool SearchForAuthors { get; set; }

        public PageViewModel Page { get; set; }

        public List<BorrowedBookServiceModel> Books { get; set; } = new List<BorrowedBookServiceModel>();

        public List<AuthorServiceModel> Authors { get; set; } = new List<AuthorServiceModel>();
    }
}