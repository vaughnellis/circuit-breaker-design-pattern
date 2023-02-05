using CircuitBreakerDesignPattern.Models;

namespace CircuitBreakerDesignPattern.HttpClients
{
    public interface ISendRequestToExternalProviderHttpClient
    {
        Task<MockResponseDTO> SendRequestByMockRequestSomething(MockRequestDTO mockRequestDTO);
    }
}
