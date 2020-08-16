using Nt.Utils.ServiceInterfaces;

namespace Nt.Utils.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public bool IsAuthenticated =>!string.IsNullOrEmpty(UserName);
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}
