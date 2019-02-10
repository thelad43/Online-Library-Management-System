namespace OnlineLibraryManagementSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class OnlineLibraryManagementSystemDbContext : IdentityDbContext
    {
        public OnlineLibraryManagementSystemDbContext(DbContextOptions<OnlineLibraryManagementSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(u => u.AuthorBooks)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);

            builder
               .Entity<User>()
               .HasMany(u => u.BorrowedBooks)
               .WithOne(b => b.Borrower)
               .HasForeignKey(b => b.BorrowerId);

            base.OnModelCreating(builder);
        }
    }
}