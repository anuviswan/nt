using System.Threading.Tasks;
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
            set
            {
                if (Equals(TypedControl.UserDisplayName, value)) return;

                TypedControl.UserDisplayName = value;
                NotifyOfPropertyChange();
                ValidateProperty(value);
            }
        }

        public string Bio
        {
            get => TypedControl.Bio;
            set
            {

                if (Equals(TypedControl.Bio, value)) return;

                TypedControl.Bio = value;
                NotifyOfPropertyChange();
                ValidateProperty(value);
            }
        }

        public async Task SaveChanges()
        {
            if (HasErrors) return;

            var request = new UpdateUserRequest
            {
                DisplayName = UserDisplayName,
                Bio = Bio
            };
            var response = await HttpService.PostAsync<UpdateUserRequest, UpdateUserResponse>(HttpUtils.UpdateUserUrl,request);
            if (response.HasError)
            {
                // TODO : Global Error Handling
            }
            TryClose(true);
        }
        
    }
}
