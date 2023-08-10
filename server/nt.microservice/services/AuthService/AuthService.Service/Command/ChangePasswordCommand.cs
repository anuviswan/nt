using MediatR;

namespace AuthService.Service.Command;

public class ChangePasswordCommand: IRequest<bool>
{
    public string UserName { get; set; } = null!;
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
