using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class UpdateUserTests : ControllerTestBase<UserProfileEntity>
    {
        public UpdateUserTests(ITestOutputHelper testOutput) : base(testOutput)
        {

        }

        #region Response Status 204
        [Theory]
        [MemberData(nameof(UpdateUser_ResponseStatus_204_TestData))]
        public async Task UpdateUser_ResponseStatus_204(UpdateUserProfileRequest request)
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "anuviswan"),

            }, "mock"));

            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.UpdateUserAsync(It.IsAny<UserProfileEntity>()))
                .Returns(Task.FromResult(true));

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);
            userController.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };
            MockModelState(request, userController);

            // Act
            var response = await userController.UpdateUser(request);

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        public static IEnumerable<object[]> UpdateUser_ResponseStatus_204_TestData => new[]
        {
            new object[]
            {
                new UpdateUserProfileRequest{Bio="Updated Bio",DisplayName="Updated DisplayName"}
            },
            new object[]
            {
                new UpdateUserProfileRequest{Bio="Updated Bio",DisplayName="Updated Display Name"}
            }
        };
        #endregion

        #region Respone Status 400
        [Theory]
        [MemberData(nameof(UpdateUser_ResponseStatus_400_TestData))]
        public async Task UpdateUser_ResponseStatus_400(UpdateUserProfileRequest request)
        {
            // Arrange
            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.UpdateUserAsync(It.IsAny<UserProfileEntity>()))
                .Returns(Task.FromResult(true));

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);
            MockModelState(request, userController);

            // Act
            var response = await userController.UpdateUser(request);

            // Assert
            var badObjectResult = Assert.IsType<BadRequestObjectResult>(response);
        }

        public static IEnumerable<object[]> UpdateUser_ResponseStatus_400_TestData => new[]
        {
            new object[]
            {
                new UpdateUserProfileRequest{Bio="Updated Bio",DisplayName=new string('s',50)}
            },
            new object[]
            {
                new UpdateUserProfileRequest{Bio=new string('s',250),DisplayName="Updated Display Name"}
            }
        };

        #endregion

    }
}
