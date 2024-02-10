namespace WebClient.Constants;

internal static class PageRoutes
{
    public const string Home = "/";

    public static class Authentication
    {
        public const string Login = "authentication/login";
        public const string Logout = "authentication/logout";
        public const string LogoutCallback = "authentication/logout-callback";
    }

    public static class Users
    {
        public const string MyClaims = "claims";
    }
}
