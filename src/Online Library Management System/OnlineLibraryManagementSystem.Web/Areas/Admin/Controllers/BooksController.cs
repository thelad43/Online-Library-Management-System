namespace OnlineLibraryManagementSystem.Web.Areas.Admin.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;
    using System.Threading.Tasks;
    using Web.Models;
    using Web.Models.Books;

    using static Common.GlobalConstants;

    public class BooksController : BaseAdminController
    {
        private readonly IBookService books;

        public BooksController(IBookService books)
        {
            this.books = books;
        }

        public async Task<IActionResult> Index(int currentPage = 1)
        {
            var books = await this.books.GetAllAsync(currentPage);

            var page = new PageViewModel
            {
                CurrentPage = currentPage,
                Area = AdminArea,
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
        public async Task<IActionResult> Edit(int id)
        {
            var book = await this.books.ByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new BookFormAdminModel
            {
                Id = id,
                Title = book.Title,
                Description = book.FullDescription
            };

            return View(model);
        }

        [HttpPost]
        [Log]
        public async Task<IActionResult> Edit(BookFormAdminModel model)
        {
            var book = await this.books.EditAsync(model.Id, model.Title, model.Description);

            TempData.AddSuccessMessage($"Successfully edited {book} book.");

            return this.RedirectToActionExtensionMethod(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await this.books.ByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new BookFormAdminModel
            {
                Id = id,
                Title = book.Title,
            };

            return View(model);
        }

        [HttpPost]
        [Log]
        public async Task<IActionResult> Destroy(int id)
        {
            var book = await this.books.DeleteAsync(id);

            TempData.AddSuccessMessage($"Successfully deleted {book} book.");

            return this.RedirectToActionExtensionMethod(nameof(Index));
        }
    }
}