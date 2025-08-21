using TestApp.Debugging;

namespace TestApp
{
    public class TestAppConsts
    {
        public const string LocalizationSourceName = "TestApp";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "ad3f9cc6fe724b39be950e85213e117b";
    }
}
