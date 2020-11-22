using Newtonsoft.Json;
using Nt.Data.Dto.BaseDto;

namespace Nt.Data.Dto.ChangePassword
{
    public class ChangePasswordRequest:BaseRequest 
    {
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
        [JsonProperty("oldPassword")]
        public string OldPassword { get; set; }
    }
}
