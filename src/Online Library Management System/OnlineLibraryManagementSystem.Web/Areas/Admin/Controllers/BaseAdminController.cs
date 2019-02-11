namespace OnlineLibraryManagementSystem.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.GlobalConstants;

    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public abstract class BaseAdminController : Controller
    {
    }
}