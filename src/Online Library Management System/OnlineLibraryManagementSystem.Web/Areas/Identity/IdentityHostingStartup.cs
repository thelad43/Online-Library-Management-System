using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(OnlineLibraryManagementSystem.Web.Areas.Identity.IdentityHostingStartup))]

namespace OnlineLibraryManagementSystem.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}