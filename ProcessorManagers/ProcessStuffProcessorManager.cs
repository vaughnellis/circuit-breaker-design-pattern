using CircuitBreakerDesignPattern.ExceptionHandlers;
using CircuitBreakerDesignPattern.HttpClients;
using CircuitBreakerDesignPattern.Models;

namespace CircuitBreakerDesignPattern.ProcessorManagers
{
    public class ProcessStuffProcessorManager : IProcessStuffProcessorManager
    {
        private readonly ISendRequestToExternalProviderHttpClient _sendRequestToExternalProviderHttpClient;
        private readonly ILogger<ProcessStuffProcessorManager> _logger;
        private readonly IExceptionHandler _exceptionHandler;

        public ProcessStuffProcessorManager(
            ISendRequestToExternalProviderHttpClient sendRequestToExternalProviderHttpClient, 
            ILogger<ProcessStuffProcessorManager> logger, 
            IExceptionHandler exceptionHandler)
        {
            _sendRequestToExternalProviderHttpClient = sendRequestToExternalProviderHttpClient;
            _logger = logger;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<MockResponseDTO> DoSomeStuff(MockRequestDTO responseDTO)
        {
            MockResponseDTO mockResponseDTO = new MockResponseDTO();
            try
            {
                //DoSomeStuff
                mockResponseDTO = await _sendRequestToExternalProviderHttpClient.SendRequestByMockRequest(responseDTO);
            }
            catch(Exception exception) 
            {
                _exceptionHandler.HandleException(exception, _logger);
            }
            return mockResponseDTO;
        }
    }
}
