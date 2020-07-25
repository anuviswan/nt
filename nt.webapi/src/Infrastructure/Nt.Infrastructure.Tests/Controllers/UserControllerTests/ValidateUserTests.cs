using MongoDB.Driver;
using Moq;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.Authentication;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.Profiles;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.RequestObjects;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ResponseObjects;
using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static System.Convert;
namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class ValidateUserTests:ControllerTestBase<UserProfileEntity>
    {
        public ValidateUserTests(ITestOutputHelper testOutput) : base(testOutput)
        {

        }

        protected override void InitializeCollection()
        {
            EntityCollection = new()
            {
                new UserProfileEntity { UserName = "AnuViswan", DisplayName = "Anu Viswan", PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyanuviswan")), IsDeleted = false },
                new UserProfileEntity { UserName = "ManuViswan", DisplayName = "Manu Viswan", PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyManuviswan")), IsDeleted = false },
                new UserProfileEntity { UserName = "AnuViswan", DisplayName = "AnuViswan", PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("userDeleted")), IsDeleted = true },
            };
        }

        [Theory]
        [MemberData(nameof(ValidateUserTestDataSuccess))]
        public async Task ValidateUserSuccess(LoginRequest loginRequest,LoginResponse loginResponse)
        {
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            mockTokenGenerator.Setup(x => x.Generate(It.IsAny<UserProfileEntity>())).Returns(It.IsAny<string>());
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.AuthenticateAsync(It.IsAny<UserProfileEntity>()))
                .Returns(Task.FromResult(EntityCollection.Single(x => x.UserName.ToLower() == loginRequest.UserName.ToLower() && x.PassKey == loginRequest.PassKey && !x.IsDeleted)));

            var userController = new UserController(Mapper,mockUserProfileService.Object,null,mockTokenGenerator.Object);
            var result = await userController.ValidateUser(loginRequest);

            if (result is IErrorInfo errorInfo && loginResponse is IErrorInfo expectedErrorInfo)
            {
                Assert.Equal(expectedErrorInfo.HasError, errorInfo.HasError);
                Assert.Equal(expectedErrorInfo.ErrorMessage, errorInfo.ErrorMessage);
                if (!errorInfo.HasError)
                {
                    Assert.Equal(loginResponse.DisplayName, result.DisplayName);
                    Assert.NotEqual(default(DateTime), result.LoginTime);
                    Assert.True(result.IsAuthenticated);
                }
            }
        }

        public static IEnumerable<object[]> ValidateUserTestDataSuccess => new List<object[]> 
        {
            new object []{new LoginRequest{UserName="AnuViswan",PassKey=ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyanuviswan"))},new LoginResponse{UserName="AnuViswan", DisplayName = "Anu Viswan",IsAuthenticated =true} },
        };


        [Theory]
        [MemberData(nameof(ValidateUserTestDataFailure))]
        public async Task ValidateUserFailure(LoginRequest loginRequest, LoginResponse loginResponse)
        {
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.AuthenticateAsync(It.IsAny<UserProfileEntity>()))
                .Throws< InvalidPasswordOrUsernameException>();

            var userController = new UserController(Mapper, mockUserProfileService.Object, null,null);
            var result = await userController.ValidateUser(loginRequest);

            if (result is IErrorInfo errorInfo && loginResponse is IErrorInfo expectedErrorInfo)
            {
                Assert.Equal(expectedErrorInfo.HasError, errorInfo.HasError);
                Assert.Equal(expectedErrorInfo.ErrorMessage, errorInfo.ErrorMessage);
                if (!errorInfo.HasError)
                {
                    Assert.Equal(loginResponse.DisplayName, result.DisplayName);
                    Assert.NotEqual(default(DateTime), result.LoginTime);
                    Assert.True(result.IsAuthenticated);
                }
            }
        }

        public static IEnumerable<object[]> ValidateUserTestDataFailure => new List<object[]>
        {
            new object []{new LoginRequest{UserName="AnuViswa",PassKey=ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyanuviswan"))},new LoginResponse{UserName="AnuViswan", DisplayName = "Anu Viswan",IsAuthenticated =false,ErrorMessage="Invalid Password or Username"} }
        };
    }
}
