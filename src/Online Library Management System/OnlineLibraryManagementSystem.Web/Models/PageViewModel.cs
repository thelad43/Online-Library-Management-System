namespace OnlineLibraryManagementSystem.Web.Models
{
    using Common;
    using System;

    public class PageViewModel
    {
        private string controller;

        public int Count { get; set; }

        public int CurrentPage { get; set; }

        public int NextPage => this.CurrentPage + 1 <= this.TotalPages ? this.CurrentPage + 1 : this.CurrentPage;

        public int PreviousPage => this.CurrentPage - 1 > 0 ? this.CurrentPage - 1 : this.CurrentPage;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(this.Count, GlobalConstants.BooksOnPage));

        public string Area { get; set; }

        public string Controller
        {
            get
            {
                return this.controller.Replace("Controller", string.Empty);
            }

            set
            {
                this.controller = value;
            }
        }

        public string Action { get; set; }

        public string SearchText { get; set; }

        public string SearchForBooks { get; set; }

        public string SearchForAuthors { get; set; }
    }
}