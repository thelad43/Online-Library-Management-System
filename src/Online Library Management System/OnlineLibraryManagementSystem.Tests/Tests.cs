namespace OnlineLibraryManagementSystem.Tests
{
    using Common.Mapping;
    using OnlineLibraryManagementSystem.Services;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                AutoMapperConfig.RegisterMappings(typeof(IService).Assembly);
                testsInitialized = true;
            }
        }
    }
}