namespace OnlineLibraryManagementSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MinLength(BookMinLength)]
        [MaxLength(BookMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public string BorrowerId { get; set; }

        public User Borrower { get; set; }
    }
}