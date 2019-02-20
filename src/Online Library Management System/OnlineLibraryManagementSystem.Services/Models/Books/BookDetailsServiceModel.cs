namespace OnlineLibraryManagementSystem.Services.Models.Books
{
    using AutoMapper;
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Models;

    public class BookDetailsServiceModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FullDescription { get; set; }

        public string Description
        {
            get
            {
                return this.FullDescription.Length <= 30
                    ? this.FullDescription : this.FullDescription.Substring(0, 30) + "...";
            }
        }

        public bool IsBorrowed { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
         => configuration.CreateMap<Book, BookDetailsServiceModel>()
                .ForMember(src => src.FullDescription, cfg => cfg.MapFrom(dest => dest.Description))
                .ForMember(src => src.IsBorrowed, cfg => cfg.MapFrom(dest => dest.BorrowerId != null));
    }
}