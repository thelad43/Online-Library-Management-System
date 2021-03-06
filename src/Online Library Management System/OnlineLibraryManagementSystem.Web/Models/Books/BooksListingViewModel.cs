﻿namespace OnlineLibraryManagementSystem.Web.Models.Books
{
    using Services.Models.Books;
    using System.Collections.Generic;

    public class AllBooksListingViewModel
    {
        public PageViewModel Page { get; set; }

        public IEnumerable<BookDetailsServiceModel> Books { get; set; }
    }
}