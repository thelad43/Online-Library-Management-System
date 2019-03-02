namespace OnlineLibraryManagementSystem.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Models.Books;
    using Services;
    using Services.Models.Authors;
    using Services.Models.Books;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IUserService users;
        private readonly IBookService books;
        private readonly IMapper mapper;

        public HomeController(IUserService users, IBookService books, IMapper mapper)
        {
            this.users = users;
            this.books = books;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authorsCount = await this.users.GetAuthorsCountAsync();
            var booksCount = await this.books.GetBooksCountAsync();
            var borrowedBooksCount = await this.books.GetBorrowedCountAsync();

            var model = new HomeIndexViewModel
            {
                AuthorsCount = authorsCount,
                BooksCount = booksCount,
                BorrowedBooksCount = borrowedBooksCount
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]SearchViewModel model, int currentPage = 1)
        {
            if (model.SearchText == null)
            {
                model.SearchText = string.Empty;
                model.SearchForBooks = true;
                model.SearchForAuthors = true;
            }

            var viewModel = new SearchViewModel
            {
                SearchText = model.SearchText,
                SearchForAuthors = model.SearchForAuthors,
                SearchForBooks = model.SearchForBooks
            };

            var booksCount = 0;
            var authorsCount = 0;

            if (!string.IsNullOrEmpty(model.SearchText))
            {
                if (model.SearchForBooks)
                {
                    var books = await this.books.SearchAsync(currentPage, model.SearchText);
                    viewModel.Books = this.mapper.Map<List<BorrowedBookServiceModel>>(books);
                    booksCount = await this.books.GetCountBySearchAsync(model.SearchText);
                }

                if (model.SearchForAuthors)
                {
                    var authors = await this.users.SearchAsync(currentPage, model.SearchText);
                    viewModel.Authors = this.mapper.Map<List<AuthorServiceModel>>(authors);
                    authorsCount = await this.users.GetCountBySearchAsync(model.SearchText);
                }
            }

            var page = new PageViewModel
            {
                Area = string.Empty,
                Controller = nameof(HomeController),
                Action = nameof(Search),
                CurrentPage = currentPage,
                Count = booksCount + authorsCount,
                SearchText = model.SearchText,
                SearchForBooks = model.SearchForBooks.ToString(),
                SearchForAuthors = model.SearchForAuthors.ToString()
            };

            viewModel.Page = page;

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Privacy() => View();

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}