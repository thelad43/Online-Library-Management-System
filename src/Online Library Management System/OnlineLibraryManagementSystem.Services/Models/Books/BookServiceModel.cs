namespace OnlineLibraryManagementSystem.Services.Models.Books
{
    using AutoMapper;
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Models;

    public class BookServiceModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string AuthorId { get; set; }

        public int BorrowedTimes { get; set; }

        public string Borrower { get; set; }

        public string BorrowerId { get; set; }

        public string FullDescription { get; set; }

        public string Description
        {
            get
            {
                return this.FullDescription.Length <= 30
                    ? this.FullDescription : this.FullDescription.Substring(0, 30) + "...";
            }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
            => configuration.CreateMap<Book, BookServiceModel>()
                .ForMember(src => src.Borrower, cfg => cfg.MapFrom(dest => dest.Borrower.UserName))
                .ForMember(src => src.BorrowerId, cfg => cfg.MapFrom(dest => dest.Borrower.Id))
                .ForMember(src => src.AuthorId, cfg => cfg.MapFrom(dest => dest.Author.Id))
                .ForMember(src => src.Author, cfg => cfg.MapFrom(dest => dest.Author.UserName))
                .ForMember(src => src.FullDescription, cfg => cfg.MapFrom(dest => dest.Description));
    }
}