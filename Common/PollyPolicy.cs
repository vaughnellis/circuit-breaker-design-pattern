using CircuitBreakerDesignPattern.ConfigurationSettings;
using CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.NoOp;

namespace CircuitBreakerDesignPattern.Common
{
    public class PollyPolicy
    {
        public readonly AsyncCircuitBreakerPolicy AsyncCircuitBreakerPolicy;
        public readonly AsyncNoOpPolicy AsyncNoOpPolicy;

        public PollyPolicy(IOptions<CircuitBreakerConfigurationSetings> optionsCircuitBreaker, ILogger<CircuitBreakerPolicy> logger)
        {
            var circuitBreakerSettings = optionsCircuitBreaker.Value;

            AsyncCircuitBreakerPolicy = Policy
                                        .Handle<GlobalTimeoutException>()
                                        .AdvancedCircuitBreakerAsync(
                                            failureThreshold: circuitBreakerSettings.FailureThreshold,
                                            samplingDuration: TimeSpan.FromMilliseconds(circuitBreakerSettings.SamplingDuration),
                                            minimumThroughput: circuitBreakerSettings.MinimumThroughput,
                                            durationOfBreak: TimeSpan.FromMilliseconds(circuitBreakerSettings.DurationOfBreak),
                                            onBreak: (exception, duration) => { logger.LogError($"CIRCUIT_BREAKER_OPEN"); },
                                            onHalfOpen: () => { logger.LogError("CIRCUIT_BREAKER_HALF_OPEN"); },
                                            onReset: () => { logger.LogError("CIRCUIT_BREAKER_CLOSED"); });

            AsyncNoOpPolicy = Policy.NoOpAsync();
        }
    }
}
