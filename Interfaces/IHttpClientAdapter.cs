namespace CircuitBreakerDesignPattern.Interfaces
{
    public interface IHttpClientAdapter
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken);
    }
}
