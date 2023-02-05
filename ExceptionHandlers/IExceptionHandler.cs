using CircuitBreakerDesignPattern.Models;

namespace CircuitBreakerDesignPattern.ExceptionHandlers
{
    public interface IExceptionHandler
    {
        void HandleException(Exception e, ILogger logger);
        Error GetError(Exception e);
    }
}
