using System;

namespace UserService.Service.Tests.Commands;
public class CreateUserCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(Handle_ValidData_Success_Data))]
    public async Task Handle_ValidData_Success(CreateUserCommand request,UserMetaInformation expectedResult)
    {
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        mockUserMetaRepository.Setup(x => x.AddAsync(It.IsAny<UserMetaInformation>()))
            .Returns<UserMetaInformation>(x => Task.FromResult(new UserMetaInformation
            {
                UserName = x.UserName
            }));


        var cts = new CancellationTokenSource();
        var handler = new CreateUserCommandHandler(mockUserMetaRepository.Object);
        var result = await handler.Handle(request, cts.Token);

        var userInfo = result.Should().BeOfType<UserMetaInformation>().Subject;
        userInfo.Should().BeEquivalentTo<UserMetaInformation>(expectedResult);

        mockUserMetaRepository.Verify(x => x.AddAsync(request.UserProfile), Times.Exactly(1));
    }


    public static IEnumerable<object[]> Handle_ValidData_Success_Data => new List<object[]>
    {
        new object[]
        {
            new CreateUserCommand
            {
                UserProfile = new UserMetaInformation
                {
                    UserName = "JiaAndNaina",
                    DisplayName = "Jia Naina"
                }
            },
            new UserMetaInformation
            {
                UserName = "JiaAndNaina"
            }
        }
    };



    [Theory]
    [MemberData(nameof(Handle_InvalidData_ThrowException_Data))]
    public async Task Handle_InvalidData_ThrowException(CreateUserCommand request)
    {
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        mockUserMetaRepository.Setup(x => x.AddAsync(It.IsAny<UserMetaInformation>()))
            .Returns<UserMetaInformation>(x => Task.FromResult(new UserMetaInformation
            {
                UserName = x.UserName
            }));


        var cts = new CancellationTokenSource();
        var handler = new CreateUserCommandHandler(mockUserMetaRepository.Object);

        await handler.Invoking(x => x.Handle(request, cts.Token))
            .Should()
            .ThrowAsync<ArgumentNullException>(); ;

        mockUserMetaRepository.Verify(x => x.AddAsync(request.UserProfile), Times.Never);
    }
    public static IEnumerable<object[]> Handle_InvalidData_ThrowException_Data => new List<object[]>
    {
        new object[]
        {
            new CreateUserCommand()
        },
        
    };
}