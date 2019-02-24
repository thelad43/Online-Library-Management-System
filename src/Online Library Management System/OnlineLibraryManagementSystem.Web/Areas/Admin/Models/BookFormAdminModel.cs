namespace OnlineLibraryManagementSystem.Web.Areas.Admin.Models
{
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Models;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class BookFormAdminModel : IMapFrom<Book>
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
    }
}