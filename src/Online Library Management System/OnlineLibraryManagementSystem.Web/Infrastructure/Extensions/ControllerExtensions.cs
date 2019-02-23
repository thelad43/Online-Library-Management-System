namespace OnlineLibraryManagementSystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExtensions
    {
        private const string Controller = "Controller";

        public static RedirectToActionResult RedirectToAction(this Controller controller, string actionName)
        {
            return controller.RedirectToAction(actionName);
        }

        public static RedirectToActionResult RedirectToAction(this Controller controller, string actionName, string controllerName)
        {
            return controller.RedirectToAction(actionName, controllerName.Replace(Controller, string.Empty));
        }

        public static RedirectToActionResult RedirectToAction(this Controller controller, string actionName, string controllerName, object routeValues)
        {
            return controller.RedirectToAction(actionName, controllerName.Replace(Controller, string.Empty), routeValues);
        }
    }
}