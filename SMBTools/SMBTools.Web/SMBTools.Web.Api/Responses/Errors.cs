namespace SMBTools.Web.Api.Responses
{
    public static class Errors
    {
        public static readonly Dictionary<Type, int> ErrorCodes = new Dictionary<Type, int>
    {
        {typeof(Exception), 600}
    };
    }
}