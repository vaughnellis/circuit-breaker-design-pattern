using CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions;
using CircuitBreakerDesignPattern.Models;
using System.Text;

namespace CircuitBreakerDesignPattern.ExceptionHandlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public Error GetError(Exception exception)
        {
            BaseException baseException = wrapException(exception);
            return new Error(baseException.ErrorCode, baseException.Message);
        }
        public void HandleException(Exception e, ILogger logger)
        {

            var baseException = wrapException(e);
            if (!string.IsNullOrEmpty(baseException.ErrorCode) && baseException.ErrorCode != "GENERAL")
            {
                logger?.LogError($"{baseException.ErrorCode}: {baseException.Message}");
            }
            else
            {
                logger?.LogError("{@Exception}", baseException);
            }
        }
        private BaseException wrapException(Exception exception)
        {
            if (exception is BaseException baseException)
            {
                return baseException;
            }
            if (exception is AggregateException aggregateException)
            {
                StringBuilder errorMessage = new StringBuilder();
                errorMessage.AppendLine(exception.Message);
                foreach (Exception innerException in aggregateException.Flatten().InnerExceptions)
                {

                    errorMessage.AppendLine(innerException.StackTrace);
                }

                return new BaseException(errorMessage.ToString(), exception);
            }
            else
            {
                return new BaseException(exception.Message, exception);
            }
        }
    }
}
