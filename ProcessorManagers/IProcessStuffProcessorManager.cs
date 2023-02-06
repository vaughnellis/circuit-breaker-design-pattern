using CircuitBreakerDesignPattern.Models;

namespace CircuitBreakerDesignPattern.ProcessorManagers
{
    public interface IProcessStuffProcessorManager
    {
        Task<MockResponseDTO> DoSomeStuff(MockRequestDTO responseDTO);
    }
}
