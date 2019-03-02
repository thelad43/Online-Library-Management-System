using Microsoft.AspNetCore.Authorization;

namespace OnlineLibraryManagementSystem.Web.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class CheckEmailModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}