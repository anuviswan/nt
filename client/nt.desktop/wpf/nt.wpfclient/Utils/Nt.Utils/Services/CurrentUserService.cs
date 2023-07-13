using System;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Nt.Data.Dto.Authenticate;
using Nt.Utils.Helper;
using Nt.Utils.ServiceInterfaces;

namespace Nt.Utils.Services;

public class CurrentUserService : ICurrentUserService
{
    public bool IsAuthenticated =>!string.IsNullOrEmpty(UserName);
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Bio { get; set; }
    public string AuthToken { get; set; }
    public async Task<bool> Authenticate(string userName, string password, NtRef<string> errorMessage)
    {
        var request = new AuthenticateRequest
        {
            Password = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(password)),
            Username = userName
        };

        var httpService = IoC.Get<IHttpService>();
        var response = await httpService.PostAsync<AuthenticateRequest, AuthenticateResponse>(HttpUtils.ValidateUserUrl, request)
            .ConfigureAwait(false);

        if (response.HasError)
        {
            errorMessage.Value = response.Errors.First();
            return false;
        }
        UserName = response.UserName;
        DisplayName = response.DisplayName;
        Bio = response.Bio;
        AuthToken = response.AuthToken;
        return true;
    }
}
