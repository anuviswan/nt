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
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;
using Xunit;
using Xunit.Abstractions;
using static System.Convert;

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
            foreach (var err in errorMessage)
            { 
                Assert.Contains(err, result.ErrorMessage.Split(Environment.NewLine)); 
            }
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

            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.UpdateUserAsync(It.IsAny<UserProfileEntity>()))
                .Returns(Task.FromResult(true));

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);
            MockModelState(request, userController);

            var result = await userController.UpdateUser(request);
            Assert.True(result is IErrorInfo instance && !instance.HasError);
            Assert.True(string.IsNullOrEmpty(result.ErrorMessage));
        }

        public static IEnumerable<object[]> UpdateUserTestSuccessTestData => new[]
        {
            new object[]{new UpdateUserProfileRequest{Bio="Updated Bio",DisplayName="Updated DisplayName"},new UpdateUserProfileResponse{} },
            new object[]{new UpdateUserProfileRequest{Bio="Updated Bio",DisplayName="Updated Display Name"}, new UpdateUserProfileResponse { } }
        };

    }
}
