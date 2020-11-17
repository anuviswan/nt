using Nt.Utils.ControlInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Controls.EditUserProfile
{
    public class EditUserProfileViewModel: NtViewModelBase<EditUserProfileControl>
    {
        public string UserName => TypedControl.UserName;
        public string UserDisplayName
        {
            get => TypedControl.UserDisplayName;
            set => TypedControl.UserDisplayName = value;
        }

        public string Bio
        {
            get => TypedControl.Bio;
            set => TypedControl.Bio = value;
        }
    }
}
