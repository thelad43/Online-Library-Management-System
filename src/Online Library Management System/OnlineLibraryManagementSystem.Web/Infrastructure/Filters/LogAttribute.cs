namespace OnlineLibraryManagementSystem.Web.Infrastructure.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.IO;

    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            using (var writer = new StreamWriter("logs.txt", true))
            {
                var log = string.Empty;

                try
                {
                    var dateTime = DateTime.UtcNow;
                    var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                    var userName = context.HttpContext.User.Identity.Name;
                    var controller = context.Controller.GetType().ToString();
                    var action = context.RouteData.Values["action"];

                    log = $"{ipAddress} - {userName} - {controller}.{action} {dateTime}";

                    if (context.Exception != null)
                    {
                        var exceptionType = context.Exception.GetType().Name;
                        var message = context.Exception.Message;

                        log = $"Exception: {log} // {exceptionType} - {message}";
                    }
                }
                catch (Exception e)
                {
                    writer.WriteLine($"Exception during logging: {e.Message} - {e.StackTrace}");
                }

                writer.WriteLine(log);
            }

            base.OnActionExecuted(context);
        }
    }
}