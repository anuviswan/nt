using Nt.Shared.Utils.ControlBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Controls.Pages.UserProfile
{
    public class UserProfileViewModel:ViewModelBase<UserProfileControl>
    {
        public string UserName 
        { 
            get=> TypedControl.UserName; 
            set=> TypedControl.UserName = value; 
        }

        public string DisplayName
        {
            get=> TypedControl.DisplayName;
            set=> TypedControl.DisplayName = value; 
        }

    }
}
