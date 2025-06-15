using UserService.Service.Services;

namespace UserService.Service.Query;

public class GetProfileImageQueryHandler : IRequestHandler<GetProfileImageQuery, byte[]?>
{
    private readonly IBlobHandlerService _blobHandlerService;
    public GetProfileImageQueryHandler(IBlobHandlerService blobHandlerService)
    {
        _blobHandlerService = blobHandlerService;
    }
    public async Task<byte[]?> Handle(GetProfileImageQuery request, CancellationToken cancellationToken)
    {
        return await _blobHandlerService.GetFile(request.UserName, cancellationToken).ConfigureAwait(false);
    }
}
