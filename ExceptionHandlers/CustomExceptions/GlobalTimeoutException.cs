namespace CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions
{
    public class GlobalTimeoutException : BaseException
    {
        public string Method { get; set; }

        public GlobalTimeoutException(string message, string method, Exception exception) 
            : base(message, exception)
        {
            ErrorCode = "GLOBAL_TIMEOUT";
            Method = method;
        }
    }
}
