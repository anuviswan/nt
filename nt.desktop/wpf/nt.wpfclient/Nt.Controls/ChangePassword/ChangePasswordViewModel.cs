using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.ChangePassword
{
    public class ChangePasswordViewModel:NtViewModelBase<ChangePasswordControl>
    {
        public string CurrentPassword
        {
            get => TypedControl.CurrentPassword;
            set => TypedControl.CurrentPassword = value;
        }

        public string NewPassword
        {
            get => TypedControl.NewPassword;
            set => TypedControl.NewPassword = value;
        }

        public string ConfirmPassword
        {
            get;
            set;
        }
    }
}
