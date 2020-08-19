using Nt.Utils.ControlInterfaces;
using System.Threading.Tasks;

namespace Nt.Controls.Login
{
    public class LoginViewModel:NtViewModelBase<LoginControl>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
        public async Task Authenticate()
        {
            if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                SetErrorState("Username or password cannot be empty");
                return;
            }
            var isAuthenticated = await TypedControl.Authenticate(UserName, Password).ConfigureAwait(false);

            if (isAuthenticated)
            {
                TryClose(true);
                return;
            }
        }

        public void SetErrorState(string message)
        {
            ErrorMessage = message;
        }
    }
}
