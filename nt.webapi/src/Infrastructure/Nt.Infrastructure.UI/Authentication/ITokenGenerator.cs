using Nt.Domain.Entities.User;

namespace Nt.Infrastructure.WebApi.Authentication;
public interface ITokenGenerator
{
    string Generate(UserProfileEntity userProfile);
}
