using Couchbase;
using Couchbase.Management.Buckets;
using Microsoft.Extensions.Options;
using ReviewService.Presenation.Api.Options;

namespace ReviewService.Presenation.Api.HostedServices;

public class EnsureBucketService : IHostedService
{
    private readonly ICluster _cluster;
    private readonly CouchbaseSettings _options;

    public EnsureBucketService(ICluster cluster, IOptions<CouchbaseSettings> options)
    {
        _cluster = cluster;
        _options = options.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var bucketManager = _cluster.Buckets;

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
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

