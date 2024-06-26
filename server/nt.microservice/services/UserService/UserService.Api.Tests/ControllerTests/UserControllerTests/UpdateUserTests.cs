﻿using MassTransit;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using UserService.Service.Dtos;

namespace UserService.Api.Tests.ControllerTests.UserControllerTests;

public class UpdateUserTests : ControllerTestBase
{
    #region 200 Tests
    [Theory]
    [MemberData(nameof(UpdateUser_ValidData_ShouldSucceed_TestData))]
    public async Task UpdateUser_ValidData_ShouldSucceed(UpdateUserRequestViewModel request, UpdateUserResponseViewModel expectedResult)
    {
        var mockMediator = new Moq.Mock<IMediator>();
        var mockMapper = new Moq.Mock<IMapper>();
        var mockPublishEndPoint = new Moq.Mock<IPublishEndpoint>()
;
        mockMediator.Setup(x => x.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()))
            .Returns<UpdateUserCommand, CancellationToken>((x, _) => Task.FromResult(new UserProfileDto
            {
                UserName = x.UserName,
                DisplayName = x.DisplayName,
                Bio = x.Bio
            }));

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "anuviswan"),

        }, "mock"));

        mockMapper.Setup(x => x.Map<UpdateUserCommand>(It.IsAny<UpdateUserRequestViewModel>())).Returns<UpdateUserRequestViewModel>(x => new UpdateUserCommand
        {
            UserName = "anuviswan",
            DisplayName = x.DisplayName,
            Bio = x.Bio,
        });

        mockMapper.Setup(x => x.Map<UpdateUserResponseViewModel>(It.IsAny<UserProfileDto>()))
            .Returns<UserProfileDto>(x => new UpdateUserResponseViewModel

        {
            DisplayName = x.DisplayName,
            Bio = x.Bio,
        });


        var nullLogger = CreateNullLogger<UserController>();

        var sut = new UserController(mockMediator.Object, mockMapper.Object, nullLogger, mockPublishEndPoint.Object);
        sut.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
        MockModelState(request, sut);
        var actualResult = await sut.UpdateUser(request);

        var okObjectResult = actualResult.Result.Should()
                           .BeOfType<OkObjectResult>()
                           .Subject;
        okObjectResult.Value.Should().BeOfType<UpdateUserResponseViewModel>();
        okObjectResult.Value.Should().BeEquivalentTo(expectedResult);
        mockMediator.Verify(x => x.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    public static IEnumerable<object[]> UpdateUser_ValidData_ShouldSucceed_TestData => new List<object[]>
    {
        new object[]
        {
            new UpdateUserRequestViewModel {  DisplayName = "Anu Viswan", Bio = "Hello, I am Anu" },
            new UpdateUserResponseViewModel { DisplayName = "Anu Viswan", Bio = "Hello, I am Anu" }
        }
    };

    #endregion

    
}
