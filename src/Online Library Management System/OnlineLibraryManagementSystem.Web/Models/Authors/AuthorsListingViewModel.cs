namespace OnlineLibraryManagementSystem.Web.Models.Authors
{
    using Services.Models.Authors;
    using System.Collections.Generic;

    public class AuthorsListingViewModel
    {
        public PageViewModel Page { get; set; }

        public IEnumerable<AuthorServiceModel> Authors { get; set; }
    }
}