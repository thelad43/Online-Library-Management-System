namespace OnlineLibraryManagementSystem.Web.Areas.Admin.Models
{
    using Services.Models.Admin;
    using System.Collections.Generic;
    using Web.Models;

    public class AllUsersListingAdminModel
    {
        public IEnumerable<UserAdminModel> Users { get; set; }

        public PageViewModel Page { get; set; }
    }
}