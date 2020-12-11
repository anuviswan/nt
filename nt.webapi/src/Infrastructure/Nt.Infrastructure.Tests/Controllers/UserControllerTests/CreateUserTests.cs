using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.CreateUser;
using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class CreateUserTests : ControllerTestBase<UserProfileEntity>
    {
        public CreateUserTests(ITestOutputHelper testOutput) : base(testOutput)
        {

        }

        protected override void InitializeCollection()
        {
            Output.WriteLine("Initialized"); //TODO: Fix this removing this line causes InitializeCollection not being called randomly
            EntityCollection = Enumerable.Range(1, 10).Select(x => new UserProfileEntity
            {
                DisplayName = $"User Name {x}",
                UserName = $"username{x}",
                PassKey = $"passKey{x}"
            }).ToList();
        }

        #region Response Status 200
        [Theory]
        [MemberData(nameof(CreateUser_ResponseStatus_200_TestData))]
        [ControllerTest(nameof(UserController)), Feature]
        public async Task CreateUser_ResponseStatus_200(CreateUserProfileRequest request, CreateUserProfileResponse expectedResult)
        {
            // Arrange
            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);

            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.CreateUserAsync(It.IsAny<UserProfileEntity>())).Returns(Task.FromResult(userProfileEntity));

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);
            MockModelState(request, userController);

            // Act
            var response = await userController.CreateUser(request);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<CreateUserProfileResponse>(okObjectResult.Value);

            Assert.Equal(expectedResult.UserName, result.UserName);
        }

        public static IEnumerable<object[]> CreateUser_ResponseStatus_200_TestData => new List<object[]>
        {
            new object[]{ new CreateUserProfileRequest { UserName = "NewUser", PassKey = "Dummy1234" }, new CreateUserProfileResponse { UserName = "NewUser" } },
        };

        #endregion

        #region Response Status 400
        [Theory]
        [MemberData(nameof(CreateUser_ResponseStatus_400_TestData))]
        [ControllerTest(nameof(UserController)), Feature]
        public async Task CreateUser_ResponseStatus_400(CreateUserProfileRequest request, SerializableError expectedResult)
        {
            // Arrange
            var mockUserProfileService = new Mock<IUserProfileService>();
            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
            mockUserProfileService.Setup(x => x.CreateUserAsync(It.IsAny<UserProfileEntity>())).Throws<UserNameExistsException>();

            var userController = new UserController(Mapper, mockUserProfileService.Object, null, null);
            MockModelState(request, userController);

            // Act
            var response = await userController.CreateUser(request);

            // Assert
            var badObjectResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            var result = Assert.IsType<SerializableError>(badObjectResult.Value);
            Assert.Equal(expectedResult, result);

        }

        public static IEnumerable<object[]> CreateUser_ResponseStatus_400_TestData => new List<object[]>
        {
            new object[]
            { 
                new CreateUserProfileRequest { UserName = "username2" },
                new SerializableError
                {
                    [nameof(CreateUserProfileRequest.PassKey)]= new[] {"Password cannot be empty"},
                }
            },
            new object[]
            { 
                new CreateUserProfileRequest { UserName = "username2",PassKey=" " },
                new SerializableError
                {
                    [nameof(CreateUserProfileRequest.PassKey)]= new[] {"Password cannot be empty"},
                } 
            },
        };
        #endregion

    }
}
