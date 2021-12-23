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
                User = new User()
                {
                    UserName = "John.Doe",
                    Password = "12345678"
                },
                Bio = "John Doe's Bio",
                DisplayName = "John Doe",
                Id = 1
            },
            new UserMetaInformation
            {
                User = new User()
                {
                    UserName = "Jane.Doe",
                    Password = "12345678"
                },
                Bio = "Jane Doe's Bio",
                DisplayName = "Jane Doe",
                Id = 1
            },
            new UserMetaInformation
            {
                User = new User()
                {
                    UserName = "Jill.Doe",
                    Password = "12345678"
                },
                Bio = "Jill Doe's Bio",
                DisplayName = "Jill Doe",
                Id = 1
            },

        };
    }

    [Theory]
    [MemberData(nameof(Handle_ValidData_Success_Data))]
    public async Task Handle_ValidData_Success(ChangePasswordCommand userToChange,UserMetaInformation expectedResult)
    {
        var mockUserRepository = new Mock<IUserMetaInformationRepository>();
        mockUserRepository.Setup(x => x.GetUser(It.IsAny<string>()))
            .Returns<string>(user=> Task.FromResult(_userCollection.FirstOrDefault(x=>x.User.UserName.ToLower() == user.ToLower())));
        mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<UserMetaInformation>()))
            .Returns<UserMetaInformation>(user => Task.FromResult(user));

        var cts = new CancellationTokenSource();
        var handler = new ChangePasswordCommandHandler(mockUserRepository.Object);
        var result = await handler.Handle(userToChange,cts.Token);

        result.Should().BeTrue();

        mockUserRepository.Verify(x => x.GetUser(It.IsAny<string>()), Times.Exactly(1));
        mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<UserMetaInformation>()), Times.Exactly(1));

    }

    public static IEnumerable<object[]> Handle_ValidData_Success_Data => new List<object[]>
    {
        new object[]
        {
            new ChangePasswordCommand
            {
                UserToUpdate = new User
                {
                    UserName = "John.Doe",
                    Password = "12345687"
                }
            },
            new UserMetaInformation
            {
                User = new User()
                {
                    UserName = "John.Doe",
                    Password = "12345687"
                },
                Bio = "John Doe's Bio",
                DisplayName = "John Doe",
                Id = 1
            }
        }
    };
}