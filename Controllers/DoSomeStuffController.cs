using CircuitBreakerDesignPattern.ExceptionHandlers;
using CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions;
using CircuitBreakerDesignPattern.Interfaces;
using CircuitBreakerDesignPattern.Models;
using CircuitBreakerDesignPattern.ProcessorManagers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CircuitBreakerDesignPattern.Controllers
{
    public class DoSomeStuffController : Controller
    {
        private readonly IProcessStuffProcessorManager _processStuffProcessorManager;
        private readonly IGlobalTimeoutAdapter _globalTimeoutAdapter;
        private readonly ILogger<DoSomeStuffController> _logger;
        private readonly IExceptionHandler _exceptionHandler;

        public DoSomeStuffController(
            IProcessStuffProcessorManager processStuffProcessorManager,
            IGlobalTimeoutAdapter globalTimeoutAdapter,
            ILogger<DoSomeStuffController> logger, 
            IExceptionHandler exceptionHandler)
        {
            _processStuffProcessorManager = processStuffProcessorManager;
            _globalTimeoutAdapter = globalTimeoutAdapter;
            _logger = logger;
            _exceptionHandler = exceptionHandler;
        }

        [HttpPost]
        public async Task<IActionResult> DoSomeStuff([FromBody] MockRequestDTO mockRequestDTO)
        {
            using (_logger.BeginScope(mockRequestDTO.MockRequestId ?? string.Empty))
            {
                var cancellationTokenSource = new CancellationTokenSource();
                var token = cancellationTokenSource.Token;

                try
                {
                    if(!ModelState.IsValid)
                    {
                        ///TO DO: Add logging
                        return BadRequest();
                    }

                    var mockResponseDTO = await _globalTimeoutAdapter.ExecuteAsync(() => 
                                                                                    _processStuffProcessorManager.DoSomeStuff(mockRequestDTO), token);
                    return Ok(mockRequestDTO);
                }
                catch(Exception exception)
                {
                    _exceptionHandler.HandleException(exception, _logger);
                    return StatusCode(500, exception.Message);
                }
            }
        }

    }
}
