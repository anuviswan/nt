using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Nt.Data.Dto.ChangePassword;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.Helper;
using Nt.Utils.ServiceInterfaces;

namespace Nt.Controls.ChangePassword;

public class ChangePasswordViewModel : NtViewModelWithHttpService<ChangePasswordControl>
{
    public ChangePasswordViewModel(IHttpService httpService) : base(httpService)
    {

    }
    public string CurrentPassword
    {
        get => TypedControl.CurrentPassword;
        set
        {
            if (Equals(TypedControl.CurrentPassword, value)) return;

            TypedControl.CurrentPassword = value;
            NotifyOfPropertyChange();
            ValidateProperty(value);
        }
    }

    public string NewPassword
    {
        get => TypedControl.NewPassword;
        set
        {
            if (Equals(TypedControl.NewPassword, value)) return;

            TypedControl.NewPassword = value;
            NotifyOfPropertyChange();
            ValidateProperty(value);
        }
    }

    public string ConfirmPassword
    {
        get => TypedControl.ConfirmPassword;
        set
        {
            if(Equals(TypedControl.ConfirmPassword, value)) return;

            TypedControl.ConfirmPassword = value; 
            NotifyOfPropertyChange();
            ValidateProperty(value);
        }
    }

    public async Task SaveChanges()
    {
        if (HasErrors) return;

        var request = new ChangePasswordRequest
        {
            NewPassword = NewPassword,
            OldPassword = CurrentPassword
        };

        var response = await HttpService.PostAsync<ChangePasswordRequest, ChangePasswordResponse>(HttpUtils.ChangePasswordUrl, request);
        if (response.HasError)
        {
            // TODO : Global Error Handling
        }
        TryClose(true);
    }
   
}
