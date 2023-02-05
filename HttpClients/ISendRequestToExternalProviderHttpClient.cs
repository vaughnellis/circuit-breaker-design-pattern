using CircuitBreakerDesignPattern.Models;

namespace CircuitBreakerDesignPattern.HttpClients
{
    public interface ISendRequestToExternalProviderHttpClient
    {
        Task<MockResponseDTO> SendRequestByMockRequest(MockRequestDTO mockRequestDTO);
    }
}
