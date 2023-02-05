using CircuitBreakerDesignPattern.ExceptionHandlers.CustomExceptions;

namespace CircuitBreakerDesignPattern.Common
{
    public static class Utility
    {
        public static HttpRequestMessage BuildHttpRequest(Uri serviceUri, string request)
        {
            if (serviceUri is null || serviceUri?.Host is null)
                throw new InvalidRequestException("Invalid service URI.");

            if(string.IsNullOrEmpty(request))
                throw new InvalidRequestException("Invalid Http content.");

            var httpRequest = new HttpRequestMessage()
            {
                RequestUri = serviceUri,
                Method = HttpMethod.Post
            };

            httpRequest.Headers.Add("Host", serviceUri.Host);

            return httpRequest;
        }

        public static void SetupHttpClient(this HttpClient client, string endpointAddress, int timeout)
        {
            client.BaseAddress = new Uri(endpointAddress);
            client.Timeout = TimeSpan.FromMilliseconds(timeout);
        }
    }
}
