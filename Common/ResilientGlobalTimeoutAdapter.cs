using CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions;
using CircuitBreakerDesignPattern.Interfaces;
using Polly.CircuitBreaker;

namespace CircuitBreakerDesignPattern.Common
{
    public class ResilientGlobalTimeoutAdapter : IGlobalTimeoutAdapter
    {
        private readonly AsyncCircuitBreakerPolicy _asyncCircuitBreakerPolicy;

        public ResilientGlobalTimeoutAdapter(PollyPolicy pollyPolicy)
        {
            _asyncCircuitBreakerPolicy = pollyPolicy.AsyncCircuitBreakerPolicy;
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> asyncMethod, CancellationToken cancellationToken)
        {
            return await _asyncCircuitBreakerPolicy.ExecuteAsync(() => ExecuteAsync(asyncMethod, cancellationToken));
        }

        private async Task<T> ExecuteMethodAsync<T>(Func<Task<T>> asyncMethod, CancellationToken cancellationToken)
        {
            try
            {
                return await asyncMethod().WithCancellation(cancellationToken);
            }
            catch(OperationCanceledException operationCanceledException)
            {
                if (!cancellationToken.IsCancellationRequested)
                    throw new GlobalTimeoutException("HttpClient Timed out.", "HttpClient", operationCanceledException);
                else
                    throw new GlobalTimeoutException("Global Timeout reached.", "ExecuteAsync", operationCanceledException);
            }
        }
    }
}
