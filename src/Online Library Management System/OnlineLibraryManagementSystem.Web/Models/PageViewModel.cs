namespace OnlineLibraryManagementSystem.Web.Models
{
    using Common;
    using System;

    public class PageViewModel
    {
        public int Count { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage => this.CurrentPage + 1 <= this.TotalPages ? this.CurrentPage + 1 : this.CurrentPage;

        public int PreviousPage => this.CurrentPage - 1 > 0 ? this.CurrentPage - 1 : this.CurrentPage;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(this.Count, GlobalConstants.BooksOnPage));

        public string Controller { get; set; }

        public string Action { get; set; }
    }
}