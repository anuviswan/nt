using System.ComponentModel.DataAnnotations;
using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.EditUserProfile;

public class EditUserProfileControl : NtControlBase<EditUserProfileViewModel>
{
    [Required]
    [StringLength(15, MinimumLength = 6, ErrorMessage ="Username should be minimum 6 characters and maximum 15")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Display Name Field cannot be empty")]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Display Name should be minimum 3 characters and maximum 30")]
    public string UserDisplayName { get; set; }

    public string Bio { get; set; }

}
