namespace OnlineLibraryManagementSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}