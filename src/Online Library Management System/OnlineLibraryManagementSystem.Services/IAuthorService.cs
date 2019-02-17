namespace OnlineLibraryManagementSystem.Services
{
    using Models.Authors;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAuthorService
    {
        Task<IEnumerable<AuthorServiceModel>> GetAllAsync(int page);

        Task<int> GetAuthorsCountAsync();
    }
}