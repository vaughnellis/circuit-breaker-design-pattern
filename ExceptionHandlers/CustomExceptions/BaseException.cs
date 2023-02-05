namespace CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions
{
    public class BaseException : Exception
    {
        public string ErrorCode { get; set; }
        public string Request { get; set; }
        public BaseException(string message, Exception e) : base(message, e)
        {
            ErrorCode = "GENERAL";
        }
        public BaseException(string message) : this(message, null)
        {

        }
    }
}
