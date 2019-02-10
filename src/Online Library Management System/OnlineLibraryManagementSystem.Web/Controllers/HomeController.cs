namespace OnlineLibraryManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using OnlineLibraryManagementSystem.Services;
    using OnlineLibraryManagementSystem.Web.Models.Books;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly IUserService users;
        private readonly IBookService books;

        public HomeController(IUserService users, IBookService books)
        {
            this.users = users;
            this.books = books;
        }

        public IActionResult Index()
        {
            var authorsCount = this.users.GetAuthorsCount();
            var booksCount = this.books.GetBooksCount();
            var borrowedBooksCount = this.books.GetBorrowedBooksCount();

            var model = new HomeIndexViewModel
            {
                AuthorsCount = authorsCount,
                BooksCount = booksCount,
                BorrowedBooksCount = borrowedBooksCount
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}