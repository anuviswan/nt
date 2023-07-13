using Newtonsoft.Json;
using Nt.Data.Dto.BaseDto;

namespace Nt.Data.Dto.UpdateUser;

public class UpdateUserRequest:BaseRequest
{
    [JsonProperty("displayName")]
    public string DisplayName { get; set; }
    [JsonProperty("bio")]
    public string Bio { get; set; }
}
