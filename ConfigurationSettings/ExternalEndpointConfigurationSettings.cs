namespace CircuitBreakerDesignPattern.ConfigurationSettings
{
    public class ExternalEndpointConfigurationSettings
    {
        public string MockExternalProviderEndpointAddress { get; set; }
        public int HttpClientTimeout { get; set; }
    }
}
