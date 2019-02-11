namespace OnlineLibraryManagementSystem.Services.Models
{
    using AutoMapper;
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Models;

    public class AuthorServiceModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
            => configuration.CreateMap<User, AuthorServiceModel>()
            .ForMember(src => src.Name, cfg => cfg.MapFrom(dest => dest.UserName))
            .ForMember(src => src.Id, cfg => cfg.MapFrom(dest => dest.Id));
    }
}