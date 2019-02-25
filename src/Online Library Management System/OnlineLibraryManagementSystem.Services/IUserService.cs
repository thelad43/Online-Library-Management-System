namespace OnlineLibraryManagementSystem.Services
{
    using Models.Admin;
    using Models.Authors;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<int> GetAuthorsCountAsync();

        Task<IEnumerable<AuthorServiceModel>> GetNonAuthorsAsync();

        Task<string> AddAuthorAsync(string userId);

        Task<IEnumerable<UserAdminModel>> GetAsync(int page);

        Task<int> GetUsersCountAsync();

        Task<IEnumerable<UserAdminModel>> SetRoleToModelAsync(IEnumerable<UserAdminModel> users);
    }
}