namespace OnlineLibraryManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Authors;
    using Services;
    using System.Threading.Tasks;

    public class AuthorsController : Controller
    {
        private readonly IAuthorService authors;

        public AuthorsController(IAuthorService authors)
        {
            this.authors = authors;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var authors = await this.authors.GetAllAsync(currentPage);

            var pageModel = new PageViewModel
            {
                CurrentPage = currentPage,
                Area = string.Empty,
                Controller = nameof(AuthorsController),
                Action = nameof(Index),
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