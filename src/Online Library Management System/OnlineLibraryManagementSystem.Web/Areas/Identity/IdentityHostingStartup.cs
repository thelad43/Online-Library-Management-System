using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineLibraryManagementSystem.Data;
using OnlineLibraryManagementSystem.Models;

[assembly: HostingStartup(typeof(OnlineLibraryManagementSystem.Web.Areas.Identity.IdentityHostingStartup))]
namespace OnlineLibraryManagementSystem.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}