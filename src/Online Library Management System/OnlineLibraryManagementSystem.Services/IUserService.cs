namespace OnlineLibraryManagementSystem.Services
{
    using Models.Authors;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<int> GetAuthorsCountAsync();

        Task<IEnumerable<AuthorServiceModel>> GetUsersAsync();

        Task<string> AddAuthorAsync(string userId);
    }
}