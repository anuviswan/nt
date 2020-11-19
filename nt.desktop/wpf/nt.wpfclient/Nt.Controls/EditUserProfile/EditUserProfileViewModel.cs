using Nt.Data.Dto.UpdateUser;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.Helper;
using Nt.Utils.ServiceInterfaces;

namespace Nt.Controls.EditUserProfile
{
    public class EditUserProfileViewModel: NtViewModelWithHttpService<EditUserProfileControl>
    {
        public EditUserProfileViewModel(IHttpService httpService):base(httpService)
        {

        }
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

        public void SaveChanges()
        {
            var request = new UpdateUserRequest
            {
                DisplayName = UserDisplayName,
                Bio = Bio
            };
            var response = HttpService.PostAsync<UpdateUserRequest, UpdateUserResponse>(HttpUtils.UpdateUserUrl,request);
        }
    }
}
