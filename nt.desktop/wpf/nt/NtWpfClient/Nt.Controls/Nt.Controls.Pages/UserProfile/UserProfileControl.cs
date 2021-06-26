using Nt.Shared.Utils.ControlBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Controls.Pages.UserProfile
{
    public class UserProfileControl:ControlBase<UserProfileViewModel>
    {
        public string UserName { get; set; } 
        public string DisplayName { get; set;  }
        public int Rating { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
