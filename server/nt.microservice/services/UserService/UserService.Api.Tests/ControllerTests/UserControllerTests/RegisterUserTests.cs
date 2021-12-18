using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using UserService.Api.Controllers;
using UserService.Api.ViewModels.User;
using UserService.Service.Command;

namespace UserService.Api.Tests.ControllerTests.UserControllerTests;

public class RegisterUserTests
{

    [Theory]
    [MemberData(nameof(RegisterUser_ValidData_ShouldSucceed_TestData))]
    public async Task RegisterUser_ValidData_ShouldSucceed(CreateUserRequestViewModel request,CreateUserResponseViewModel expectedResult)
    {
        var cancellationToken = default(CancellationToken);
        var mockMediator = new Moq.Mock<IMediator>();
        mockMediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(),It.IsAny<CancellationToken>()))
            .Returns<CreateUserCommand,CancellationToken>((x,_)=> Task.FromResult(new UserMetaInformation
            {
                User = new User() { UserName = x.User.User.UserName },
                DisplayName = x.User.DisplayName
            }));
        
        var mockMapper = new Moq.Mock<IMapper>();
        mockMapper.Setup(x => x.Map<UserMetaInformation>(It.IsAny<CreateUserRequestViewModel>())).Returns<CreateUserRequestViewModel>(x => new UserMetaInformation
        {
            User = new User
            {
                UserName = x.UserName,
                Password = x.Password
            },
            DisplayName = x.DisplayName,
            Bio = x.Bio,
        });

        mockMapper.Setup(x => x.Map<CreateUserResponseViewModel>(It.IsAny<UserMetaInformation>())).Returns<UserMetaInformation>(x => new CreateUserResponseViewModel
        {
            UserName = x.User.UserName,
        });


        var userController = new UserController(mockMediator.Object, mockMapper.Object);
        var actualResult = await userController.RegisterUser(request);

        var okObjectResult  = actualResult.Result.Should()
                           .BeOfType<OkObjectResult>()
                           .Subject;
        okObjectResult.Value.Should().BeOfType<CreateUserResponseViewModel>();
        okObjectResult.Value.Should().BeEquivalentTo(expectedResult);
    }

    public static IEnumerable<object[]> RegisterUser_ValidData_ShouldSucceed_TestData => new List<object[]>
    {
        new object[]
        {
            new CreateUserRequestViewModel { UserName = "SampleUser", Password = "Dummy1234" },
            new CreateUserResponseViewModel { UserName = "SampleUser" }
        }
    };
}