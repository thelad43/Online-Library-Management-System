namespace OnlineLibraryManagementSystem.Services
{
    using Models.Authors;
    using Models.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<int> GetAuthorsCountAsync();

        Task<IEnumerable<AuthorServiceModel>> GetUsersAsync();

        Task<string> AddAuthorAsync(string userId);

        Task<IEnumerable<UserAdminModel>> GetUsersAsync(int page);
        
        Task<int> GetUsersCountAsync();

        Task<IEnumerable<UserAdminModel>> SetRoleToModelAsync(IEnumerable<UserAdminModel> users);
    }
}