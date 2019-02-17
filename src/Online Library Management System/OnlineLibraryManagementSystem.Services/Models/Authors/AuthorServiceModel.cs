namespace OnlineLibraryManagementSystem.Services.Models.Authors
{
    using AutoMapper;
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Models;

    public class AuthorServiceModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int BooksCount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
            => configuration.CreateMap<User, AuthorServiceModel>()
                .ForMember(src => src.Id, cfg => cfg.MapFrom(dest => dest.Id))
                .ForMember(src => src.Name, cfg => cfg.MapFrom(dest => dest.UserName))
                .ForMember(src => src.BooksCount, cfg => cfg.MapFrom(dest => dest.AuthorBooks.Count));
    }
}