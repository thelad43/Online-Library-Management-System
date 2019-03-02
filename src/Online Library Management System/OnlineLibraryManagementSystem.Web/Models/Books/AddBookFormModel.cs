namespace OnlineLibraryManagementSystem.Web.Models.Books
{
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class AddBookFormModel
    {
        private const string TitleMinLengthErrorMessage = "Title must be at least 2 symbols!";
        private const string TitleMaxLengthErrorMessage = "Title cannot be more than 30 symbols!";

        private const string DescriptionMinLengthErrorMessage = "Description must be at least 10 symbols!";
        private const string DescriptionMaxLengthErrorMessage = "Description cannot be more than 100 symbols!";

        [Required]
        [MinLength(BookMinLength, ErrorMessage = TitleMinLengthErrorMessage)]
        [MaxLength(BookMaxLength, ErrorMessage = TitleMaxLengthErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthErrorMessage)]
        [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthErrorMessage)]
        public string Description { get; set; }
    }
}