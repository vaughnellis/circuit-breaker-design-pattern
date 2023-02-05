using CircuitBreakerDesignPattern.Common;
using CircuitBreakerDesignPattern.ConfigurationSettings;
using CircuitBreakerDesignPattern.ExceptionHandlers;
using CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions;
using CircuitBreakerDesignPattern.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CircuitBreakerDesignPattern.HttpClients
{
    public class SendRequestToExternalProviderHHttpClient : ISendRequestToExternalProviderHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly Uri _serviceUri;
        private readonly ExternalEndpointConfigurationSettings _externalEndpointConfigurationSettings;
        private readonly ILogger<SendRequestToExternalProviderHHttpClient> _logger;
        private readonly IExceptionHandler _exceptionHandler;

        public SendRequestToExternalProviderHHttpClient(
            HttpClient httpClient,
            IOptions<ExternalEndpointConfigurationSettings> externalConfigurationSettings,
            ILogger<SendRequestToExternalProviderHHttpClient> logger,
            IExceptionHandler exceptionHandler)
        {
            _httpClient = httpClient;
            _externalEndpointConfigurationSettings = externalConfigurationSettings.Value;
            _exceptionHandler = exceptionHandler;
            _logger = logger;
        }

        public async Task<MockResponseDTO> SendRequestByMockRequest(MockRequestDTO mockRequestDTO)
        {
            if (mockRequestDTO is null)
                throw new InvalidRequestException("MockRequestDTO should not be null.");

            MockResponseDTO mockResponseDTO = null;

            try
            {
                var request = Utility.BuildHttpRequest(_serviceUri, JsonConvert.SerializeObject(mockResponseDTO));

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var faultResponse = await response.Content.ReadAsStringAsync();
                    throw new ProviderException(string.IsNullOrEmpty(faultResponse) ? "SendRequestByMockRequest failed." : faultResponse);
                }

                //Dirty way of mocking
                var result = JsonConvert.SerializeObject(
                    new MockResponseDTO() { MockResponseSomething = "Something", MockResponseSomething2 = "Something2"});

                //Unwrap
                mockResponseDTO = JsonConvert.DeserializeObject<MockResponseDTO>(result);
            }
            catch (Exception)
            {
                throw;
            }

            return mockResponseDTO;
        }
    }
}
