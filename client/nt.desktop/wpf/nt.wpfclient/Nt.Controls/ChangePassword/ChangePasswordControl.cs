using Nt.Utils.ControlInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Controls.ChangePassword
{
    public class ChangePasswordControl: NtControlBase<ChangePasswordViewModel>
    {
        [Required(ErrorMessage = "Password cannot be empty")]
        [MinLength(8, ErrorMessage = "Password should contain minimum of 8 characters")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Password cannot be empty")]
        [MinLength(8, ErrorMessage = "Password should contain minimum of 8 characters")]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
    }
}
