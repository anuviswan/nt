using AuthService.Api.ViewModels.Validate;

namespace AuthService.Api.Authentication;
public interface ITokenGenerator
{
    string Generate(string userName);
}

