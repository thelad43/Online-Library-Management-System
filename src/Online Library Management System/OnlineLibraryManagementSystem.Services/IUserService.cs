namespace OnlineLibraryManagementSystem.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        int GetAuthorsCount();

        Task<IEnumerable<AuthorServiceModel>> GetUsers();

        Task<string> AddAuthor(string userId);
    }
}