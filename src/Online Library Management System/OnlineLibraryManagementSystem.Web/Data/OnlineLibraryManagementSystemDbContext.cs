namespace OnlineLibraryManagementSystem.Web.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class OnlineLibraryManagementSystemDbContext : IdentityDbContext
    {
        public OnlineLibraryManagementSystemDbContext(DbContextOptions<OnlineLibraryManagementSystemDbContext> options)
            : base(options)
        {
        }
    }
}