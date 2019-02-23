namespace OnlineLibraryManagementSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using OnlineLibraryManagementSystem.Services;
    using OnlineLibraryManagementSystem.Web.Areas.Admin.Models;
    using OnlineLibraryManagementSystem.Web.Controllers;
    using OnlineLibraryManagementSystem.Web.Infrastructure.Extensions;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseAdminController
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        [HttpGet]
        public async Task<IActionResult> AddAuthor()
        {
            var users = await this.users.GetUsersAsync();

            var model = new AdminUsersListingModel
            {
                Users = users.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id
                }),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(string userId)
        {
            var userName = await this.users.AddAuthorAsync(userId);

            TempData.AddSuccessMessage($"Successfully added role 'Author' to user {userName}.");

            return this.RedirectToAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }
    }
}