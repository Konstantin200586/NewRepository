namespace SMBTools.Contract.Helpers;

public static class RouteHelper
{
    private const string BasePath = "api";

    public static string GetPath(string controller)
    {
        return Path.Combine(BasePath, controller);
    }

    public static string GetPath(string controller, string action)
    {
        return Path.Combine(BasePath, controller, action);
    }
}