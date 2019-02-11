namespace OnlineLibraryManagementSystem.Web.Models.Books
{
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class AddBookFormModel
    {
        [Required]
        [MinLength(BookMinLength, ErrorMessage = "Title must be at least 2 symbols!")]
        [MaxLength(BookMaxLength, ErrorMessage = "Title cannot be more than 30 symbols!")]
        public string Title { get; set; }

        [Required]
        [MinLength(DescriptionMinLength, ErrorMessage = "Description must be at least 10 symbols!")]
        [MaxLength(DescriptionMaxLength, ErrorMessage = "Description cannot be more than 100 symbols!")]
        public string Description { get; set; }
    }
}