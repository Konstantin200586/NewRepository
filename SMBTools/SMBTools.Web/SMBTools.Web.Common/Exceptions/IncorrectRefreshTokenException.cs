namespace SMBTools.Web.Common.Exceptions
{
    public class IncorrectRefreshTokenException : Exception
    {
        private const string ExceptionMessage = "IncorrectRefreshToken";

        public IncorrectRefreshTokenException() : base(ExceptionMessage)
        {

        }
    }
}