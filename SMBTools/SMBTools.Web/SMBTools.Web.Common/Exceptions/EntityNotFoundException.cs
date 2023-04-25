namespace SMBTools.Web.Common.Exceptions
{
    public class EntityNotFoundException<T> : Exception where T : class
    {
        public const string ExceptionMessage = "Entity {0} was not found";

        public EntityNotFoundException() : base(string.Format(ExceptionMessage, typeof(T).Name))
        {
        }
    }
}