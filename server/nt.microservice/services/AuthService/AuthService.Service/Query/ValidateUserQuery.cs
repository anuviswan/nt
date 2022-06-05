using AuthService.Domain.Entities;
using MediatR;

namespace AuthService.Service.Query;
public class ValidateUserQuery:IRequest<User>
{
    public User? User { get; set; }
}
