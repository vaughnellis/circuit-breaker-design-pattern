using CircuitBreakerDesignPattern.Interfaces;

namespace CircuitBreakerDesignPattern.Common
{
    public class HttpClientAdapter : IHttpClientAdapter
    {
        public HttpClient HttpClient { get; set; }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await HttpClient.SendAsync(request, cancellationToken);
        }
    }
}
