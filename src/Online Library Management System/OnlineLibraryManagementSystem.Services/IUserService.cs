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

        Task<IEnumerable<UserAdminModel>> GetAllAsync(int page);

        Task<int> GetUsersCountAsync();

        Task<IEnumerable<UserAdminModel>> SetRoleToModelAsync(IEnumerable<UserAdminModel> users);

        Task<IEnumerable<AuthorServiceModel>> SearchAsync(int page, string searchText);

        Task<int> GetCountBySearchAsync(string searchText);
    }
}