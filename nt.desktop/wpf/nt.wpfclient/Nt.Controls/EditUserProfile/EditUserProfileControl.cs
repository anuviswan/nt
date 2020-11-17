using Nt.Utils.ControlInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Controls.EditUserProfile
{
    public class EditUserProfileControl : NtControlBase<EditUserProfileViewModel>
    {
        public string UserName { get; set; }
        public string UserDisplayName { get; set; }
        public string Bio { get; set; }

    }
}
