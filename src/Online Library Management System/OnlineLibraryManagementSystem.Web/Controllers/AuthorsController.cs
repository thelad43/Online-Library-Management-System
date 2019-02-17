namespace OnlineLibraryManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using OnlineLibraryManagementSystem.Services;
    using OnlineLibraryManagementSystem.Web.Models;
    using OnlineLibraryManagementSystem.Web.Models.Authors;
    using System.Threading.Tasks;

    public class AuthorsController : Controller
    {
        private readonly IAuthorService authors;

        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }

        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var authors = await this.authors.GetAllAsync(currentPage);

            var pageModel = new PageViewModel
            {
                CurrentPage = currentPage,
                Controller = "Authors",
                Action = "Index",
                Count = await this.authors.GetAuthorsCountAsync()
            };

            var model = new AuthorsListingViewModel
            {
                Authors = authors,
                Page = pageModel
            };

            return View(model);
        }
    }
}