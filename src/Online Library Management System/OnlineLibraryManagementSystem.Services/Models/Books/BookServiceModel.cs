namespace OnlineLibraryManagementSystem.Services.Models.Books
{
    using AutoMapper;
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Models;

    public class BookServiceModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int BorrowedTimes { get; set; }

        public string Author { get; set; }

        public string Borrower { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
          => configuration.CreateMap<Book, BookServiceModel>()
                .ForMember(src => src.Author, cfg => cfg.MapFrom(dest => dest.Author.UserName))
                .ForMember(src => src.Borrower, cfg => cfg.MapFrom(dest => dest.Borrower.UserName));
    }
}