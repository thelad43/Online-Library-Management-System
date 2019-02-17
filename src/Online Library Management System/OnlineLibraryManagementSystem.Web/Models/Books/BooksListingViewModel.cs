﻿namespace OnlineLibraryManagementSystem.Web.Models.Books
{
    using Services.Models.Books;
    using System.Collections.Generic;

    public class BooksListingViewModel
    {
        public PageViewModel Page { get; set; }

        public IEnumerable<ShortBookServiceModel> Books { get; set; }
    }
}