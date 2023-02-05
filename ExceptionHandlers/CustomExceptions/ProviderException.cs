namespace CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions
{
    public class ProviderException : BaseException
    {
        public object ProviderMessage { get; }
        public ProviderException(string message, object request) : base(message)
        {
            ErrorCode = "PROVIDER_ERROR";
            ProviderMessage = request;
        }
        public ProviderException(string message) : this(message, null) { }
    }
}
