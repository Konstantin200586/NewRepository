namespace SMBTools.Web.Common.Exceptions
{
    public class UserCredentialIsNotValidException : Exception
    {
        private const string ExceptionMessage = "Invalid user login or password.";
        public UserCredentialIsNotValidException()
            : base(ExceptionMessage) { }
    }
}
