namespace UserService.Service.Query;

public class GetProfileImageQuery : IRequest<byte[]>
{
    public required string UserName { get; set; }
}
