using CircuitBreakerDesignPattern.Models;

namespace CircuitBreakerDesignPattern.HttpClients
{
    public class SendRequestToExternalProviderHHttpClient : ISendRequestToExternalProviderHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _serviceUri;
        private readonly ILogger<SendRequestToExternalProviderHHttpClient> _logger;

        public SendRequestToExternalProviderHHttpClient(
            HttpClient httpClient,
            ILogger<SendRequestToExternalProviderHHttpClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public Task<MockResponseDTO> SendRequestByMockRequestSomething(MockRequestDTO mockRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
