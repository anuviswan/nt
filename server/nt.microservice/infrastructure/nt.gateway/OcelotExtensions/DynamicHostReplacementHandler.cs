public class DynamicHostReplacementHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
        if (Environment.GetEnvironmentVariable("RUNNING_WITH")?.ToUpper() == "ASPIRE")
        {
            request.RequestUri = new Uri($"http://localhost:{request.RequestUri.Port}" + request.RequestUri.PathAndQuery); // Set the base URL for the request
        }
            
        var response = await base.SendAsync(request, token);
        // Do post-processing of the response...
        return response;
    }


}
public class LoggingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
        Console.WriteLine($">>> Ocelot resolved downstream: {request.RequestUri}");
        return await base.SendAsync(request, token);
    }
}