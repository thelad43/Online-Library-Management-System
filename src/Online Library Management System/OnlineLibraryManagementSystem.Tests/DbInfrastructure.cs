namespace OnlineLibraryManagementSystem.Tests
{
    using Microsoft.EntityFrameworkCore;
    using OnlineLibraryManagementSystem.Data;
    using System;

    public class DbInfrastructure
    {
        public static OnlineLibraryManagementSystemDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<OnlineLibraryManagementSystemDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new OnlineLibraryManagementSystemDbContext(dbOptions);
        }
    }
}