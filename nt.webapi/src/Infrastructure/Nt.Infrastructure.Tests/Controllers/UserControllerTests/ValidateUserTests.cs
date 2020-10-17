using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.Authentication;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.Profiles;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser;
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
                new UserProfileEntity { UserName = "AnuViswan", DisplayName = "Anu Viswan", PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyanuviswan")), IsDeleted = false ,Bio="UserBio"},
                new UserProfileEntity { UserName = "ManuViswan", DisplayName = "Manu Viswan", PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyManuviswan")), IsDeleted = false, Bio = "UserBio" },
                new UserProfileEntity { UserName = "AnuViswan", DisplayName = "AnuViswan", PassKey = ToBase64String(ASCIIEncoding.ASCII.GetBytes("userDeleted")), IsDeleted = true, Bio = "UserBio" },
            };
        }

        #region Response Status 400
        [Theory]
        [MemberData(nameof(ValidateUser_ResponseStatus_200_TestData))]
        public async Task ValidateUser_ResponseStatus_200(LoginRequest loginRequest, LoginResponse expectedResult)
        {
            // Arrange
            var mockTokenGenerator = new Mock<ITokenGenerator>();
            mockTokenGenerator.Setup(x => x.Generate(It.IsAny<UserProfileEntity>())).Returns(It.IsAny<string>());
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.AuthenticateAsync(It.IsAny<UserProfileEntity>()))
                .Returns(Task.FromResult(EntityCollection.Single(x => x.UserName.ToLower() == loginRequest.UserName.ToLower() && x.PassKey == loginRequest.PassKey && !x.IsDeleted)));

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, mockTokenGenerator.Object);

            // Act
            var response = await userController.ValidateUser(loginRequest);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<LoginResponse>(okObjectResult.Value);

            Assert.Equal(expectedResult.DisplayName, result.DisplayName);
            Assert.NotEqual(default(DateTime), result.LoginTime);
            Assert.True(result.IsAuthenticated);
            Assert.Equal(expectedResult.Bio, result.Bio);
        }

        public static IEnumerable<object[]> ValidateUser_ResponseStatus_200_TestData => new List<object[]>
        {
            new object []
            {
                new LoginRequest{UserName="AnuViswan",PassKey=ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyanuviswan"))},
                new LoginResponse{UserName="AnuViswan", DisplayName = "Anu Viswan",IsAuthenticated =true,Bio="UserBio"} 
            },
        };
        #endregion


        #region Response Status 400
        [Theory]
        [MemberData(nameof(ValidateUser_ResponseStatus_400_TestData))]
        public async Task ValidateUser_ResponseStatus_400(LoginRequest loginRequest, string expectedErrorMessage)
        {
            // Arrange
            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.AuthenticateAsync(It.IsAny<UserProfileEntity>()))
                .Throws<InvalidPasswordOrUsernameException>();

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);

            // Act
            var response = await userController.ValidateUser(loginRequest);

            //Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            var actualErrorMessage = Assert.IsType<string>(badRequestObjectResult.Value);
            Assert.Equal(expectedErrorMessage, actualErrorMessage);
        }

        public static IEnumerable<object[]> ValidateUser_ResponseStatus_400_TestData => new List<object[]>
        {
            new object []
            {
                new LoginRequest{UserName="AnuViswa",PassKey=ToBase64String(ASCIIEncoding.ASCII.GetBytes("passkeyanuviswan"))},
                "Invalid Password or Username"
            },
             new object []
             {
                 new LoginRequest{UserName=string.Empty,PassKey=string.Empty},
                 "Invalid Password or Username"
             }
        };
        #endregion





    }
}
