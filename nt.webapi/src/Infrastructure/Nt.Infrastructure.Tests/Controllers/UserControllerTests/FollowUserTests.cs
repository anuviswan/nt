using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.FollowUser;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class FollowUserTests : ControllerTestBase<UserProfileEntity>
    {
        public FollowUserTests(ITestOutputHelper testOutput) : base(testOutput)
        {

        }

        protected override void InitializeCollection()
        {
            base.InitializeCollection();
            EntityCollection = new List<UserProfileEntity>(MockDataHelper.UserCollection);
        }

        #region Response Status 204
        [Theory]
        [MemberData(nameof(FollowUser_ResponseStatus_204_TestData))]
        [ControllerTest(nameof(UserController)), Feature]
        public async Task FollowUser_ResponseStatus_204(FollowUserRequest request)
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, Utils.GenerateUserIdString(1)),

            }, "mock"));

            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.FollowUserAsync(It.IsAny<string>(),It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);
            userController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            MockModelState(request, userController);

            // Act
            var response = await userController.FollowUser(request);

            // Assert
            Assert.IsType<NoContentResult>(response);
            mockUserManagementService.Verify(x => x.FollowUserAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        public static IEnumerable<object[]> FollowUser_ResponseStatus_204_TestData => new[]
        {
            new object[]
            {
                new FollowUserRequest{UserToFollow =  Utils.GenerateUserIdString(2)}
            },
            new object[]
            {
                new FollowUserRequest{UserToFollow =  Utils.GenerateUserIdString(5)}
            }
        };
        #endregion

        #region Respone Status 400
        [Theory]
        [MemberData(nameof(FollowUser_ResponseStatus_400_TestData))]
        [ControllerTest(nameof(UserController)), Feature]
        public async Task FollowUser_ResponseStatus_400(FollowUserRequest request)
        {
            // Arrange
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, Utils.GenerateUserIdString(1)),

            }, "mock"));

            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.FollowUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);
            userController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
            MockModelState(request, userController);

            // Act
            var response = await userController.FollowUser(request);

            // Assert
            var badObjectResult = Assert.IsType<BadRequestObjectResult>(response);
            mockUserManagementService.Verify(x => x.FollowUserAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        public static IEnumerable<object[]> FollowUser_ResponseStatus_400_TestData => new[]
        {
            new object[]
            {
                 new FollowUserRequest{UserToFollow =  null}
            },
            new object[]
            {
                new FollowUserRequest{UserToFollow =  string.Empty}
            }
        };

        #endregion
    }
}
