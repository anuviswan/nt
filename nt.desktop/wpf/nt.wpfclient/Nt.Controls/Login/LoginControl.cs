using Caliburn.Micro;
using Nt.Data.Dto.Authenticate;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.Helper;
using Nt.Utils.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Controls.Login
{
    public class LoginControl : NtControlBase<LoginViewModel>
    {
        public LoginControl():base()
        {

        }

        public async Task<bool> Authenticate(string userName,string password)
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
                ViewModel.SetErrorState(response.ErrorMessage);
                return false;
            }
            var currentUserService = IoC.Get<ICurrentUserService>();
            currentUserService.UserName = response.UserName;
            currentUserService.DisplayName = response.DisplayName;
            return true;
        }
    }
}
