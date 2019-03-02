namespace OnlineLibraryManagementSystem.Web.Areas.Admin.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Services;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Controllers;
    using Web.Models;

    public class UsersController : BaseAdminController
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var users = await this.users.GetAllAsync(currentPage);

            users = await this.users.SetRoleToModelAsync(users);

            var page = new PageViewModel
            {
                CurrentPage = currentPage,
                Area = string.Empty,
                Controller = nameof(UsersController),
                Action = nameof(Index),
                Count = await this.users.GetUsersCountAsync()
            };

            var model = new AllUsersListingAdminModel
            {
                Page = page,
                Users = users
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddAuthor()
        {
            var users = await this.users.GetNonAuthorsAsync();

            var model = new UsersFormListingAdminModel
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
        [Log]
        public async Task<IActionResult> AddAuthor(string userId)
        {
            var userName = await this.users.AddAuthorAsync(userId);

            TempData.AddSuccessMessage($"Successfully added role 'Author' to user {userName}.");

            return this.RedirectToActionExtensionMethod(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }
    }
}