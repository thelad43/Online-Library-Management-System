namespace OnlineLibraryManagementSystem.Common
{
    public static class GlobalConstants
    {
        public const int BookMinLength = 2;
        public const int BookMaxLength = 30;

        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 100;

        public const string AdministratorRole = "Administrator";
        public const string AuthorRole = "Author";
        public const string UserRole = "User";

        public const string AdminArea = "Admin";

        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";

        public const int BooksOnPage = 10;
        public const int AuthorsOnPage = 20;
        public const int UsersCountOnPage = 15;
    }
}