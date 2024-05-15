using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;

namespace UserService.Service.Services;

public interface IBlobHandlerService
{
    Task<Azure.Response<BlobContentInfo>> UploadFile(MemoryStream memoryStream, string blobFileName, CancellationToken cancellationToken = default);
    Task<byte[]> GetFile(string blobFileName, CancellationToken cancellationToken = default);
}

public class BlobHandlerService : IBlobHandlerService
{
    private const string ConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
    private const string ContainerName = "democontainer";

    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobContainerClient _blobContainerClient;

    public BlobHandlerService()
    {
        _blobServiceClient = new BlobServiceClient(ConnectionString);
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
    }
    public async Task<byte[]> GetFile(string blobFileName, CancellationToken cancellationToken = default)
    {
        var blobClient = _blobContainerClient.GetBlobClient(blobFileName);
        var response = await blobClient.DownloadContentAsync();
        return response.Value.Content.ToArray();
    }

    public async Task<Response<BlobContentInfo>> UploadFile(MemoryStream memoryStream, string blobFileName, CancellationToken cancellationToken = default)
    {
        await _blobContainerClient.CreateIfNotExistsAsync().ConfigureAwait(false);
        var response = await _blobContainerClient.UploadBlobAsync(blobFileName, memoryStream, cancellationToken);
        return response;
    }
}
