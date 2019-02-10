namespace OnlineLibraryManagementSystem.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public List<Book> AuthorBooks { get; set; } = new List<Book>();

        public List<Book> BorrowedBooks { get; set; } = new List<Book>();
    }
}