using UserService.Service.Dtos;
using UserService.Service.Services;

namespace UserService.Service.Command;

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, ProfileImageDto>
{
    private readonly IBlobHandlerService _blobHandlerService;
    public UploadImageCommandHandler(IBlobHandlerService blobHandlerService)
    {
        _blobHandlerService = blobHandlerService;
    }
    public async Task<ProfileImageDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        request.FileData.CopyTo(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        var result = await _blobHandlerService.UploadFile(memoryStream, request.ImageKey, cancellationToken).ConfigureAwait(false);

        return new()
        {
            ImageKey = request.ImageKey,
        };

    }
}
