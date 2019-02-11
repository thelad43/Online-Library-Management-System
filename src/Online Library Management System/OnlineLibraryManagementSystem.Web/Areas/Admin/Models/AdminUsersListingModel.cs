namespace OnlineLibraryManagementSystem.Web.Areas.Admin.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class AdminUsersListingModel
    {
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}