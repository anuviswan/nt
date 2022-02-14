using AuthService.Domain.Entities;
using MediatR;

namespace AuthService.Service.Command;
public class AddUserCommand:IRequest<User>
{
    public User User { get; set; }
}
