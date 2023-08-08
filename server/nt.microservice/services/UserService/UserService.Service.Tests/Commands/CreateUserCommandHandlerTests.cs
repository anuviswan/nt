using AutoMapper;
using System;
using UserService.Service.Dtos;

namespace UserService.Service.Tests.Commands;
public class CreateUserCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(Handle_ValidData_Success_Data))]
    public async Task Handle_ValidData_Success(CreateUserCommand request,UserProfileDto expectedResult)
    {
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        var mockMapper = new Mock<IMapper>();

        mockUserMetaRepository.Setup(x => x.AddAsync(It.IsAny<UserMetaInformation>()))
            .Returns<UserMetaInformation>(x => Task.FromResult(new UserMetaInformation
            {
                UserName = x.UserName,
                Bio = x.Bio,
                DisplayName = x.DisplayName,
            }));

        mockMapper.Setup(x=>x.Map<UserProfileDto>(It.IsAny<UserMetaInformation>()))
            .Returns<UserMetaInformation>((x) =>
            new UserProfileDto {
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
        var handler = new CreateUserCommandHandler(mockUserMetaRepository.Object,mockMapper.Object);
        var result = await handler.Handle(request, cts.Token);

        var userInfo = result.Should().BeOfType<UserProfileDto>().Subject;
        userInfo.Should().BeEquivalentTo<UserProfileDto>(expectedResult);

        mockUserMetaRepository.Verify(x => x.AddAsync(It.Is<UserMetaInformation>
            ((s) => s.UserName == request.UserName 
            && s.DisplayName == request.DisplayName 
            && s.Bio == request.Bio )), Times.Exactly(1));
    }


    public static IEnumerable<object[]> Handle_ValidData_Success_Data => new List<object[]>
    {
        new object[]
        {
            new CreateUserCommand
            {
                UserName = "JiaAndNaina",
                DisplayName = "Jia Naina",
                Bio = "We are Jia and Naina"
            },
            new UserProfileDto
            {
                UserName = "JiaAndNaina",
                DisplayName = "Jia Naina",
                Bio = "We are Jia and Naina"
            }
        }
    };



    [Theory]
    [MemberData(nameof(Handle_InvalidData_ThrowException_Data))]
    public async Task Handle_InvalidData_ThrowException(CreateUserCommand request)
    {
        var mockUserMetaRepository = new Mock<IUserMetaInformationRepository>();
        var mockMapper = new Moq.Mock<IMapper>();

        mockUserMetaRepository.Setup(x => x.AddAsync(It.IsAny<UserMetaInformation>()))
            .Returns<UserMetaInformation>(x => Task.FromResult(new UserMetaInformation
            {
                UserName = x.UserName
            }));

        mockMapper.Setup(x => x.Map<UserProfileDto>(It.IsAny<UserMetaInformation>()))
                .Returns<UserMetaInformation>((x) =>
                new UserProfileDto
                {
                    UserName = x.UserName,
                    Bio = x.Bio,
                    DisplayName = x.DisplayName,
                });

        var cts = new CancellationTokenSource();
        var handler = new CreateUserCommandHandler(mockUserMetaRepository.Object, mockMapper.Object);

        await handler.Invoking(x => x.Handle(request, cts.Token))
            .Should()
            .ThrowAsync<ArgumentNullException>(); ;

        mockUserMetaRepository.Verify(x => x.AddAsync(new UserMetaInformation
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
            new CreateUserCommand()
        },
        
    };
}