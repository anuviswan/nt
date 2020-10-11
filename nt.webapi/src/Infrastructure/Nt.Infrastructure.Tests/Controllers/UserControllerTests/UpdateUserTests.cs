using Microsoft.AspNetCore.Http;
using Moq;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.Authentication;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.UpdateUser;
using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using Xunit;
using Xunit.Abstractions;
using static System.Convert;
using System.Web;
using System.Security.Principal;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class UpdateUserTests : ControllerTestBase<UserProfileEntity>
    {
        public UpdateUserTests(ITestOutputHelper testOutput) : base(testOutput)
        {

        }

        [Theory]
        [MemberData(nameof(UpdateUserTestErrorTestData))]
        public async Task UpdateUserTestError(UpdateUserProfileRequest request, params string[] errorMessage)
        {

            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.UpdateUserAsync(It.IsAny<UserProfileEntity>()))
                .Returns(Task.FromResult(true));

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);
            MockModelState(request,userController);

            var result = await userController.UpdateUser(request);
            Assert.True(errorMessage.All(result.Errors.Contains));
            Assert.True(result.HasError);
        }

        public static IEnumerable<object[]> UpdateUserTestErrorTestData => new[]
        {
            new object[]{new UpdateUserProfileRequest{Bio="Updated Bio",DisplayName=new string('s',50)},"Display Name should be less than 30 characters"},
            new object[]{new UpdateUserProfileRequest{Bio=new string('s',250),DisplayName="Updated Display Name"}, "Bio should be less than 180 characters" }
        };


        [Theory]
        [MemberData(nameof(UpdateUserTestSuccessTestData))]
        public async Task UpdateUserTestSuccess(UpdateUserProfileRequest request, UpdateUserProfileResponse expectedResponse)
        {
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

            var result = await userController.UpdateUser(request);
            Assert.False(result.HasError);
            Assert.False(result.Errors.Any());
        }

        public static IEnumerable<object[]> UpdateUserTestSuccessTestData => new[]
        {
            new object[]{new UpdateUserProfileRequest{Bio="Updated Bio",DisplayName="Updated DisplayName"},new UpdateUserProfileResponse{} },
            new object[]{new UpdateUserProfileRequest{Bio="Updated Bio",DisplayName="Updated Display Name"}, new UpdateUserProfileResponse { } }
        };

    }
}
