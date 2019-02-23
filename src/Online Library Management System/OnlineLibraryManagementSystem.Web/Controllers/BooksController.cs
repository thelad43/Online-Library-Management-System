namespace OnlineLibraryManagementSystem.Web.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Books;
    using OnlineLibraryManagementSystem.Models;
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
                Controller = nameof(BooksController),
                Action = nameof(Index),
                Count = await this.books.GetBooksCountAsync()
            };

            var model = new AllBooksListingViewModel
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

            if (user == null)
            {
                return NotFound();
            }

            await this.books.AddAsync(model.Title, model.Description, user.Id);

            TempData.AddSuccessMessage($"Successfully added book {model.Title} by {user.UserName}");

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ByAuthor(string id, int currentPage = 1)
        {
            var books = await this.books.ByAuthorAsync(id, currentPage);

            var page = new PageViewModel
            {
                CurrentPage = currentPage,
                Controller = nameof(BooksController),
                Action = nameof(ByAuthor),
                Count = await this.books.GetBooksByAuthorCountAsync(id)
            };

            var author = await this.userManager.FindByIdAsync(books?.FirstOrDefault()?.AuthorId);

            if (author == null)
            {
                return NotFound();
            }

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
            => View(await this.books.ByIdAsync(id));

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Borrow(int id)
        {
            var userName = User.Identity.Name;

            var book = await this.books.BorrowAsync(id, userName);

            if (book == null)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage($"Successfully borrowed {book} book.");

            return RedirectToAction(nameof(MyBorrowedBooks));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Return(int id)
        {
            var userName = User.Identity.Name;

            var book = await this.books.ReturnAsync(id, userName);

            if (book == null)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage($"Successfully returned {book} book to the library.");

            return RedirectToAction(nameof(MyBorrowedBooks));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyBorrowedBooks(int currentPage = 1)
        {
            var userName = User.Identity.Name;

            var books = await this.books.MyBorrowedAsync(userName, currentPage);

            var page = new PageViewModel
            {
                CurrentPage = currentPage,
                Controller = nameof(BooksController),
                Action = nameof(MyBorrowedBooks),
                Count = await this.books.GetMyBorrowedBooksCountAsync(userName)
            };

            var model = new BorrowedBooksListingViewModel
            {
                Page = page,
                Books = books
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Borrowed(int currentPage = 1)
        {
            var books = await this.books.BorrowedAsync(currentPage);

            var page = new PageViewModel
            {
                CurrentPage = currentPage,
                Controller = nameof(BooksController),
                Action = nameof(Borrowed),
                Count = await this.books.GetBorrowedBooksCountAsync()
            };

            var model = new BorrowedBooksListingViewModel
            {
                Page = page,
                Books = books
            };

            return View(model);
        }
    }
}