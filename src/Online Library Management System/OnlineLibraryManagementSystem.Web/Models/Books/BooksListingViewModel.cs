namespace OnlineLibraryManagementSystem.Web.Models.Books
{
    using Common;
    using Services.Models.Books;
    using System;
    using System.Collections.Generic;

    public class BooksListingViewModel
    {
        public int AllBooksCount { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage => this.CurrentPage + 1 <= this.TotalPages ? this.CurrentPage + 1 : this.CurrentPage;

        public int PreviousPage => this.CurrentPage - 1 > 0 ? this.CurrentPage - 1 : this.CurrentPage;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(this.AllBooksCount, GlobalConstants.BooksOnPage));

        public IEnumerable<ShortBookServiceModel> Books { get; set; }
    }
}