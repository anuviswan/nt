using Microsoft.Extensions.Options;
using ReviewService.Presenation.Api.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ReviewService.Presenation.Api.HostedServices;
public class ClusterBootstrapService : IHostedService
{
    private readonly ILogger<ClusterBootstrapService> _logger;
    private readonly HttpClient _httpClient = new();

    private string CouchbaseHost => _settings.ConnectionString.Replace("couchbase://", "http://"); // for REST API
    private string Username => _settings.Username;
    private string Password => _settings.Password;

    private string ClusterName => _settings.ClusterName;
    private readonly CouchbaseSettings _settings;

    public ClusterBootstrapService(IOptions<CouchbaseSettings> settings, ILogger<ClusterBootstrapService> logger)
    {
        _logger = logger;
        _settings = settings.Value ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting Couchbase cluster bootstrap...");

        // crude wait — replace with health check polling if needed
        await Task.Delay(5000, cancellationToken);

        try
        {
            if (!await IsNodeFullyInitialized())
            {
                await SetupServices();
                await ConfigureClusterMemory();
                await SetupAdminCredentials();
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                                           "Basic",
                                           Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{Password}")));

            await RenameNode();
            _logger.LogInformation("Couchbase cluster bootstrap complete.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cluster bootstrap failed");
        }
    }

    private async Task<bool> IsNodeFullyInitialized()
    {
        var response = await _httpClient.GetAsync($"{CouchbaseHost}/pools/default");
        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);
        var node = doc.RootElement.GetProperty("nodes")[0];
        var services = node.GetProperty("services").EnumerateArray().Select(e => e.GetString()).ToList();
        return services.Contains("mgmt") && services.Contains("n1ql") && services.Contains("index");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private async Task SetupServices()
    {
        await PostForm($"{CouchbaseHost}/node/controller/setupServices", new Dictionary<string, string>
        {
            { "services", "kv,n1ql,index,mgmt" }
        });
    }

    private async Task ConfigureClusterMemory()
    {
        await PostForm($"{CouchbaseHost}/pools/default", new Dictionary<string, string>
        {
            { "memoryQuota", "256" },
            { "indexMemoryQuota", "256" },
            { "clusterName", ClusterName },
            { "ftsMemoryQuota", "256" }
        });

        //await PostForm($"{CouchbaseHost}/nodes/self/controller/settings", new Dictionary<string, string>
        //{
        //    { "path", "/opt/couchbase/var/lib/couchbase" },
        //    { "index_path", "/opt/couchbase/var/lib/couchbase" }
        //});

      
    }

    private async Task RenameNode()
    {
        //await PostForm($"{CouchbaseHost}/node/controller/rename", new Dictionary<string, string>
        //{
        //    { "hostname", "nt-reviewservice-db" } // Or: "nt-reviewservice-db" if using that container name in Aspire
        //});
    }
    private async Task SetupAdminCredentials()
    {
        await PostForm($"{CouchbaseHost}/settings/web", new Dictionary<string, string>
        {
            { "username", Username },
            { "password", Password },
            { "port", "8091" }
        });
    }

    private async Task PostForm(string url, Dictionary<string, string> formData, bool addAuth = true)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new FormUrlEncodedContent(formData)
        };

        if (addAuth && _httpClient.DefaultRequestHeaders.Authorization == null)
        {
            var byteArray = Encoding.ASCII.GetBytes($"{Username}:{Password}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            _logger.LogError("POST to {Url} failed with status {StatusCode}: {Body}", url, response.StatusCode, body);
            response.EnsureSuccessStatusCode(); // will throw
        
        }
        else
        {
            _logger.LogInformation("POST {Url} succeeded", url);
        }
    }
}
