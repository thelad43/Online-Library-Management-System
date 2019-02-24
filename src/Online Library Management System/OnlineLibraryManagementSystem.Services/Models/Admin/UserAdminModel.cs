namespace OnlineLibraryManagementSystem.Services.Models.Admin
{
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Models;

    public class UserAdminModel : IMapFrom<User>
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}