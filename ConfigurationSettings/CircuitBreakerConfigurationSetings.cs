namespace CircuitBreakerDesignPattern.ConfigurationSettings
{
    public class CircuitBreakerConfigurationSetings
    {
        public double FailureThreshold { get; set; }
        public double SamplingDuration { get; set; }
        public int MinimumThroughput { get; set; }
        public double DurationOfBreak { get; set; }
    }
}
