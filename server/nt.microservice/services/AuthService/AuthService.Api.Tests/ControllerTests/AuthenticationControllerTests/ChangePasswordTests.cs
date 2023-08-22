using AuthService.Api.Authentication;
using AuthService.Api.Controllers;
using AuthService.Api.ViewModels.ChangePassword;
using AuthService.Service.Command;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Api.Tests.ControllerTests.AuthenticationControllerTests;

[TestFixture]
public class ChangePasswordTests : ControllerTestsBase
{
    private IMediator _mediator = null!;
    private IMapper _mapper = null!;

    [SetUp]
    public void Initialize()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
    }

    [Test]
    [TestCaseSource(nameof(ChangePassword_ValidData_ChangesPasswordSuccess_TestData))]
    public async Task ChangePassword_ValidData_ChangesPasswordSuccess(ChangePasswordRequestViewModel request)
    {
        #region Arrange
        _mapper.Map<ChangePasswordCommand>(Arg.Any<ChangePasswordRequestViewModel>()).Returns(c => new ChangePasswordCommand
        {
            NewPassword = request.NewPassword,
            OldPassword = request.OldPassword,
            UserName = "anuviswan"
        });

        _mediator.Send(Arg.Any<ChangePasswordCommand>()).Returns(c=>true);
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "anuviswan"),

        }, "mock"));
        


        var tokenGenerator = Substitute.For<ITokenGenerator>();
        tokenGenerator.Generate(Arg.Any<string>()).Returns(s => "ThisIsADemoAuthToken");

        var logger = CreateNullLogger<AuthenticationController>();
        #endregion

        #region Act
        var sut = new AuthenticationController(_mapper,_mediator,tokenGenerator,logger);
        sut.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
        MockModelState(request, sut);
        var result = await sut.ChangePassword(request).ConfigureAwait(false);
        #endregion

        #region Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result is NoContentResult);
        #endregion
    }


    static IEnumerable<object> ChangePassword_ValidData_ChangesPasswordSuccess_TestData()
    {
        yield return new object[]
        {
            new ChangePasswordRequestViewModel
            {
                NewPassword = "12345678",
                OldPassword = "87654321"
            }
        };
    }

}
