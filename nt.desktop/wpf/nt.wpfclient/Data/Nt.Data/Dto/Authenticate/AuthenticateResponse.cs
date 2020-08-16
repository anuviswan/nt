using Nt.Data.Dto.BaseDto;

namespace Nt.Data.Dto.Authenticate
{
    public class AuthenticateResponse:BaseResponse
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}
