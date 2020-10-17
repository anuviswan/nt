using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ChangePassword;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public  class ChangePasswordTests : ControllerTestBase<UserProfileEntity>
    {
        public ChangePasswordTests(ITestOutputHelper testOutput) : base(testOutput)
        {

        }

        #region Response Status 204
        [Theory]
        [MemberData(nameof(ChangePassword_ResponseStatus_204_TestData))]
        public async Task ChangePassword_ResponseStatus_204(ChangePasswordRequest request)
        { 
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "anuviswan"),

            }, "mock"));


            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.ChangePasswordAsync(It.IsAny<UserProfileEntity>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);
            userController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
            MockModelState(request, userController);

            // Act
            var response = await userController.ChangePassword(request);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        public static IEnumerable<object[]> ChangePassword_ResponseStatus_204_TestData => new[]
        {
            new object[]{new ChangePasswordRequest { OldPassword="OldPassworld",NewPassword="NewPassword"}},
        };

        #endregion

        #region Response Status 400
        [Theory]
        [MemberData(nameof(ChangePassword_ResponseStatus_400_TestData))]
        public async Task ChangePassword_ResponseStatus_400(ChangePasswordRequest request)
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "anuviswan"),

            }, "mock"));


            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.ChangePasswordAsync(It.IsAny<UserProfileEntity>(), It.IsAny<string>()))
                .Throws<InvalidPasswordOrUsernameException>();

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);
            userController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
            MockModelState(request, userController);

            // Act
            var response = await userController.ChangePassword(request);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        public static IEnumerable<object[]> ChangePassword_ResponseStatus_400_TestData => new[]
        {
            new object[]{new ChangePasswordRequest { OldPassword = "Sample", NewPassword = "1" }},
            new object[]{new ChangePasswordRequest { OldPassword = "WrongPassword", NewPassword = "SampleNew" }},
        };
        #endregion




        
    }
}
