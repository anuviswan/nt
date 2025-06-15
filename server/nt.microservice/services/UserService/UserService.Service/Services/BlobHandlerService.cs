using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using nt.shared.dto.Attributes;

namespace UserService.Service.Services;

public class BlobHandlerService : IBlobHandlerService
{
    [TechnicalDebt(DebtType.BadDesign,"Read connection string from configuration file")]
    private const string ContainerName = "democontainer";

    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;

    public BlobHandlerService(string connectionString)
    {
        var options = new BlobClientOptions()
        {
            Diagnostics = { IsLoggingContentEnabled = true, IsLoggingEnabled = true, IsTelemetryEnabled = false }
        };

        _blobServiceClient = new BlobServiceClient(connectionString, options);
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
    }
    public async Task<byte[]?> GetFile(string blobFileName, CancellationToken cancellationToken = default)
    {
        var blobClient = _blobContainerClient.GetBlobClient(blobFileName);
        if(await blobClient.ExistsAsync(cancellationToken))
        {
            var response = await blobClient.DownloadContentAsync().ConfigureAwait(false);
            return response.Value.Content.ToArray();
        }
        return default;
    }

    public async Task<Response<BlobContentInfo>> UploadFile(MemoryStream memoryStream, string blobFileName, CancellationToken cancellationToken = default)
    {
        await _blobContainerClient.CreateIfNotExistsAsync().ConfigureAwait(false);
        var response = await _blobContainerClient.UploadBlobAsync(blobFileName, memoryStream, cancellationToken);
        return response;
    }
}
