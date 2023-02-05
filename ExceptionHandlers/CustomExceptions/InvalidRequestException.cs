namespace CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions
{
    public class InvalidRequestException : BaseException
    {
        public object InvalidRequest { get; }
        public InvalidRequestException(string message, object request) : base(message)
        {
            ErrorCode = "INVALID_REQUEST";
            InvalidRequest = request;
        }
        public InvalidRequestException(string message) : this(message, null) { }
    }
}
