namespace SalesWebMvc.Services.Exceptions
{
    public class ValidateLoginException : ApplicationException
    {
        public ValidateLoginException(string message) : base(message)
        {
        }
    }
}
