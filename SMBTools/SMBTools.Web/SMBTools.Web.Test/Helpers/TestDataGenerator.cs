namespace SMBTools.Web.Test.Helpers
{
    public static class TestDataGenerator
    {
        private static readonly Random _random = new Random();
        public static string GetString() => Guid.NewGuid().ToString("N");
        public static int GetInt() => _random.Next();
    }
}
