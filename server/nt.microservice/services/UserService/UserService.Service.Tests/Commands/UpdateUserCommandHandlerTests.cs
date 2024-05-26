using AutoMapper;
using System;
using UserService.Service.Dtos;

namespace UserService.Service.Tests.Commands;

public class UpdateUserCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(Handle_ValidData_Success_Data))]
    public async Task Handle_ValidData_Success(UpdateUserCommand request, UserProfileDto currentData, UserProfileDto expectedResult)
    {
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        var mockMapper = new Mock<IMapper>();

        mockUserMetaRepository.Setup(x=> x.GetUser(It.IsAny<string>()))
            .Returns<string?>(x => Task.FromResult<UserMetaInformation?>(new UserMetaInformation
            {
                UserName = currentData.UserName,
                Bio = currentData.Bio,
                DisplayName = currentData.DisplayName,
            }));
        mockUserMetaRepository.Setup(x => x.UpdateAsync(It.IsAny<UserMetaInformation>()))
            .Returns<UserMetaInformation>(x => Task.FromResult(new UserMetaInformation
            {
                UserName = expectedResult.UserName,
                Bio = expectedResult.Bio,
                DisplayName = expectedResult.DisplayName,
            }));

        mockMapper.Setup(x => x.Map<UserProfileDto>(It.IsAny<UserMetaInformation>()))
            .Returns<UserMetaInformation>((x) =>
            new UserProfileDto
            {
                UserName = x.UserName,
                Bio = x.Bio,
                DisplayName = x.DisplayName,
            });

        mockMapper.Setup(x => x.Map<UserMetaInformation>(It.IsAny<CreateUserCommand>()))
                .Returns<CreateUserCommand>((x) =>
                new UserMetaInformation
                {
                    UserName = x.UserName,
                    Bio = x.Bio,
                    DisplayName = x.DisplayName,
                });


        var cts = new CancellationTokenSource();
        var sut = new UpdateUserCommandHandler(mockUserMetaRepository.Object,mockMapper.Object);
        var result = await sut.Handle(request, cts.Token);

        var userInfo = result.Should().BeOfType<UserProfileDto>().Subject;
        userInfo.Should().BeEquivalentTo<UserProfileDto>(expectedResult);

        mockUserMetaRepository.Verify(x => x.UpdateAsync(It.Is<UserMetaInformation>
            ((s) => s.UserName == request.UserName
            && s.DisplayName == request.DisplayName
            && s.Bio == request.Bio)), Times.Exactly(1));
    }


    public static IEnumerable<object[]> Handle_ValidData_Success_Data => new List<object[]>
    {
        new object[]
        {
            new UpdateUserCommand
            {
                UserName = "JiaAndNaina",
                DisplayName = "Jia Anu & Naina Anu",
                Bio = "Hello, We are Jia& Naina"
            },
            new UserProfileDto
            {
                UserName = "JiaAndNaina",
                DisplayName = "Jia Naina",
                Bio = "We are Jia and Naina"
            },
            new UserProfileDto
            {
                UserName = "JiaAndNaina",
                DisplayName = "Jia Anu & Naina Anu",
                Bio = "Hello, We are Jia& Naina"
            }
        }
    };



    [Theory]
    [MemberData(nameof(Handle_InvalidData_ThrowException_Data))]
    public async Task Handle_InvalidData_ThrowException(UpdateUserCommand request)
    {
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        var mockMapper = new Moq.Mock<IMapper>();

        mockUserMetaRepository.Setup(x => x.UpdateAsync(It.IsAny<UserMetaInformation>()));

        mockMapper.Setup(x => x.Map<UserProfileDto>(It.IsAny<UserMetaInformation>()))
                .Returns<UserMetaInformation>((x) =>
                new UserProfileDto
                {
                    UserName = x.UserName,
                    Bio = x.Bio,
                    DisplayName = x.DisplayName,
                });

        var cts = new CancellationTokenSource();
        var sut = new UpdateUserCommandHandler(mockUserMetaRepository.Object,mockMapper.Object);

        await sut.Invoking(x => x.Handle(request, cts.Token))
            .Should()
            .ThrowAsync<ArgumentException>(); ;

        mockUserMetaRepository.Verify(x => x.UpdateAsync(new UserMetaInformation
        {
            UserName = request.UserName,
            Bio = request.Bio,
            DisplayName = request.DisplayName,
        }), Times.Never);
    }
    public static IEnumerable<object[]> Handle_InvalidData_ThrowException_Data => new List<object[]>
    {
        new object[]
        {
            new UpdateUserCommand()
            {
                UserName = string.Empty
            }
        },

    };
}
