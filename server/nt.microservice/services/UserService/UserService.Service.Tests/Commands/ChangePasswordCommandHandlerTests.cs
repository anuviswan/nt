using System;
using System.Linq;

namespace UserService.Service.Tests.Cmmands;
public class ChangePasswordCommandHandlerTests
{
    private List<UserMetaInformation> _userCollection;

    public ChangePasswordCommandHandlerTests()
    {
        _userCollection = new List<UserMetaInformation>()
        {
            new UserMetaInformation
            {
                UserName = "John.Doe",
                Bio = "John Doe's Bio",
                DisplayName = "John Doe",
                Id = 1
            },
            new UserMetaInformation
            {
                UserName = "Jane.Doe",
                Bio = "Jane Doe's Bio",
                DisplayName = "Jane Doe",
                Id = 1
            },
            new UserMetaInformation
            {
                UserName = "Jill.Doe",
                Bio = "Jill Doe's Bio",
                DisplayName = "Jill Doe",
                Id = 1
            },

        };
    }

    
}