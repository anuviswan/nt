using Caliburn.Micro;
using Nt.Data.Dto.Authenticate;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.Helper;
using Nt.Utils.ServiceInterfaces;
using System.Threading.Tasks;

namespace Nt.Controls.Login
{
    public class LoginViewModel:NtViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public async Task Authenticate()
        {
            if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                //TODO: Raise Error MEssage
            }

            var request = new AuthenticateRequest
            {
                Password = Password,
                Username = UserName
            };

            var httpService = IoC.Get<IHttpService>();
            var response = await httpService.PostAsync<AuthenticateRequest,AuthenticateResponse>(HttpUtils.ValidateUserUrl, request);
        }


    }
}
