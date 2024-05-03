namespace ApiCatalog.Core.Exceptions
{
    public class PasswordRecoveryException : Exception
    {
        public PasswordRecoveryException(string message)
            : base(message)
        { }
    }
}