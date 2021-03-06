﻿namespace OnlineLibraryManagementSystem.Services
{
    using Models.Books;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookService
    {
        Task<int> GetBooksCountAsync();

        Task<int> GetBorrowedCountAsync();

        Task<IEnumerable<BookDetailsServiceModel>> GetAllAsync(int page);

        Task AddAsync(string title, string description, string id);

        Task<BookServiceModel> ByIdAsync(int id);

        Task<IEnumerable<BookServiceModel>> ByAuthorAsync(string id, int page);

        Task<int> GetByAuthorCountAsync(string id);

        Task<string> BorrowAsync(int id, string userName);

        Task<IEnumerable<BorrowedBookServiceModel>> MyBorrowedAsync(string userName, int page);

        Task<int> GetMyBorrowedCountAsync(string userName);

        Task<string> ReturnAsync(int id, string userName);

        Task<IEnumerable<BorrowedBookServiceModel>> BorrowedAsync(int page);

        Task<string> EditAsync(int id, string title, string description);

        Task<string> DeleteAsync(int id);

        Task<IEnumerable<BorrowedBookServiceModel>> SearchAsync(int page, string searchText);

        Task<int> GetCountBySearchAsync(string searchText);
    }
}