using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using nt.shared.dto.Attributes;

namespace UserService.Service.Services;

public class BlobHandlerService : IBlobHandlerService
{
    [TechnicalDebt(DebtType.BadDesign,"Read connection string from configuration file")]
    private const string ConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://ntuserserviceblobstorage:10000/devstoreaccount1;";
    private const string ContainerName = "democontainer";

    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;

    public BlobHandlerService()
    {
        var options = new BlobClientOptions()
        {
            Diagnostics = { IsLoggingContentEnabled = true, IsLoggingEnabled = true, IsTelemetryEnabled = false }
        };

        _blobServiceClient = new BlobServiceClient(ConnectionString, options);
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
    }
    public async Task<byte[]> GetFile(string blobFileName, CancellationToken cancellationToken = default)
    {
        var blobClient = _blobContainerClient.GetBlobClient(blobFileName);
        var response = await blobClient.DownloadContentAsync().ConfigureAwait(false);
        return response.Value.Content.ToArray();
    }

    public async Task<Response<BlobContentInfo>> UploadFile(MemoryStream memoryStream, string blobFileName, CancellationToken cancellationToken = default)
    {
        await _blobContainerClient.CreateIfNotExistsAsync().ConfigureAwait(false);
        var response = await _blobContainerClient.UploadBlobAsync(blobFileName, memoryStream, cancellationToken);
        return response;
    }
}
