using AuthService.Api.Authentication;
using AuthService.Api.Controllers;
using AuthService.Api.ViewModels.ChangePassword;
using MapsterMapper;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public async Task ChangePassword_ValidData_ChangesPasswordSuccess(ChangePasswordRequestViewModel request)
    {
        #region Arrange
        var tokenGenerator = Substitute.For<ITokenGenerator>();
        tokenGenerator.Generate(Arg.Any<string>()).Returns(s => "ThisIsADemoAuthToken");

        var logger = CreateNullLogger<AuthenticationController>();
        #endregion

        #region Act
        var sut = new AuthenticationController(_mapper,_mediator,tokenGenerator,logger);
        var result = await sut.ChangePassword(request).ConfigureAwait(false);
        #endregion

        #region Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result is OkObjectResult);
        #endregion
    }
}
