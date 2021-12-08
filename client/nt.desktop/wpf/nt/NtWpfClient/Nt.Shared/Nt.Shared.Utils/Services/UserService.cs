using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Data.Types.Dto.Authenticate;
using Nt.Shared.Utils.Helpers;
using Nt.Shared.Utils.ServiceInterfaces;

namespace Nt.Shared.Utils.Services
{
    public class UserService:IUserService
    {
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserName);
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string AuthToken { get; set; }
        public async Task<bool> Authenticate(string userName, string password, AsyncRef<string> errorMessage)
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
}
