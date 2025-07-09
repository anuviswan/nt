using Couchbase;
using Couchbase.Management.Buckets;
using Microsoft.Extensions.Options;
using ReviewService.Presenation.Api.Options;

namespace ReviewService.Presenation.Api.HostedServices;

public class EnsureBucketService : IHostedService
{
    private readonly ICluster _cluster;
    private readonly CouchbaseSettings _options;
    private readonly ILogger<EnsureBucketService> _logger;

    public EnsureBucketService(ICluster cluster, IOptions<CouchbaseSettings> options, ILogger<EnsureBucketService> logger)
    {
        _cluster = cluster;
        _options = options.Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        int retries = 10;
        var bucketManager = _cluster.Buckets;
        while (retries-- > 0)
        {
            try
            {
                _logger.LogInformation("Ensuring bucket '{BucketName}' exists...", _options.BucketName);

                var allBuckets = await bucketManager.GetAllBucketsAsync();
                if (!allBuckets.ContainsKey(_options.BucketName))
                {
                    var settings = new BucketSettings
                    {
                        Name = _options.BucketName,
                        BucketType = BucketType.Couchbase,
                        RamQuotaMB = 100,
                        FlushEnabled = true
                    };
                    await bucketManager.CreateBucketAsync(settings);
                    break;
                }
                else
                {
                    _logger.LogInformation("Bucket '{BucketName}' already exists.", _options.BucketName);
                    break;
                }
            }
            catch (ServiceNotAvailableException)
            {
                _logger.LogWarning("Couchbase service not available, retrying in 2 seconds...");
                await Task.Delay(2000);
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

