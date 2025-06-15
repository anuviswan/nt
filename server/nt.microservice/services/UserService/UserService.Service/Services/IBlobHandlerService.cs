using Azure.Storage.Blobs.Models;

namespace UserService.Service.Services;

public interface IBlobHandlerService
{
    Task<Azure.Response<BlobContentInfo>> UploadFile(MemoryStream memoryStream, string blobFileName, CancellationToken cancellationToken = default);
    Task<byte[]?> GetFile(string blobFileName, CancellationToken cancellationToken = default);
}
