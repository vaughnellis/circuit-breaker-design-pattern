namespace CircuitBreakerDesignPattern.Interfaces
{
    public interface IGlobalTimeoutAdapter
    {
        Task<T> ExecuteAsync<T>(Func<Task<T>> asyncMethod, CancellationToken cancellationToken);
    }
}
