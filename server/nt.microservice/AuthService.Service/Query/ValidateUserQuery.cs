using AuthService.Domain.Entities;
using MediatR;

namespace AuthService.Service.Query;
public class ValidateUserQuery:IRequest<bool>
{
    public User User { get; set; }
}
