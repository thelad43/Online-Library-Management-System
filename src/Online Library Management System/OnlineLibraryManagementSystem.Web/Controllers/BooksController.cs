namespace OnlineLibraryManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Books;
    using OnlineLibraryManagementSystem.Models;
    using OnlineLibraryManagementSystem.Web.Infrastructure.Extensions;
    using OnlineLibraryManagementSystem.Web.Models;
    using Services;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.GlobalConstants;

    public class BooksController : Controller
    {
        private readonly IBookService books;
        private readonly UserManager<User> userManager;

        public BooksController(IBookService books, UserManager<User> userManager)
        {
            this.books = books;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var books = await this.books.GetAllAsync(currentPage);

            var page = new PageViewModel
            {
                CurrentPage = currentPage,
                Controller = "Books",
                Action = "Index",
                Count = await this.books.GetBooksCountAsync()
            };

            var model = new BooksListingViewModel
            {
                Books = books,
                Page = page
            };

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = AdministratorRole + "," + AuthorRole)]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = AdministratorRole + "," + AuthorRole)]
        public async Task<IActionResult> Add(AddBookFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.GetUserAsync(User);

            await this.books.AddAsync(model.Title, model.Description, user.Id);

            TempData.AddSuccessMessage($"Successfully added book {model.Title} by {user.UserName}");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ByAuthor(string id, int currentPage = 1)
        {
            var books = await this.books.ByAuthorAsync(id, currentPage);

            var page = new PageViewModel
            {
                CurrentPage = currentPage,
                Controller = "Books",
                Action = "ByAuthor",
                Count = await this.books.GetBooksByAuthorCountAsync(id)
            };

            var author = await userManager.FindByIdAsync(books.First().AuthorId);

            var model = new BooksByAuthorListingViewModel
            {
                Books = books,
                Page = page,
                Author = author.UserName
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await this.books.ByIdAsync(id);

            return View(book);
        }
    }
}