using Moq;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.RequestObjects;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ResponseObjects;
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
    public class RegisterUserTests : ControllerTestBase<UserProfileEntity>
    {
        public RegisterUserTests(ITestOutputHelper testOutput) : base(testOutput)
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

        [Theory]
        [MemberData(nameof(RegisterUserTestData))]
        public async Task RegisterUserTest(CreateUserProfileRequest request,CreateUserProfileResponse expectedResult)
        {
            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);

            var mockUserProfileService = new Mock<IUserProfileService>();
            mockUserProfileService.Setup(x => x.CreateUserAsync(It.IsAny<UserProfileEntity>())).Returns(Task.FromResult(userProfileEntity));

            var userController = new UserController(Mapper,mockUserProfileService.Object,null);
            var result = await userController.CreateUser(request);

            if (result is IErrorInfo errorInfo && expectedResult is IErrorInfo expectedErrorInfo)
            {
                Assert.Equal(expectedErrorInfo.HasError, errorInfo.HasError);
                Assert.Equal(expectedErrorInfo.ErrorMessage, errorInfo.ErrorMessage);
            }
            else
            {
                // TODO: Fail here
            }
            
            Assert.Equal(expectedResult.UserName, result.UserName);

        }

        public static IEnumerable<object[]> RegisterUserTestData => new List<object[]>
        {
            new object[]{ new CreateUserProfileRequest { UserName = "username2" },new CreateUserProfileResponse { UserName = "username2", ErrorMessage = "Password cannot be empty or whitespace" } },
            new object[]{ new CreateUserProfileRequest { UserName = "username2",PassKey=" " },new CreateUserProfileResponse { UserName = "username2", ErrorMessage = "Password cannot be empty or whitespace" } },
            new object[]{ new CreateUserProfileRequest { UserName = "NewUser", PassKey = "Dummy" }, new CreateUserProfileResponse { UserName = "NewUser" } },
        };


        [Theory]
        [MemberData(nameof(RegisterUserTestDataUserExistException))]
        public async Task RegisterUserTestUserExistException(CreateUserProfileRequest request, CreateUserProfileResponse expectedResult)
        {
            var mockUserProfileService = new Mock<IUserProfileService>();
            var userProfileEntity = Mapper.Map<UserProfileEntity>(request);
            mockUserProfileService.Setup(x => x.CreateUserAsync(It.IsAny<UserProfileEntity>())).Throws<UserNameExistsException>();

            var userController = new UserController(Mapper, mockUserProfileService.Object, null);
            var result = await userController.CreateUser(request);

            if (result is IErrorInfo errorInfo && expectedResult is IErrorInfo expectedErrorInfo)
            {
                Assert.Equal(expectedErrorInfo.HasError, errorInfo.HasError);
                Assert.Equal(expectedErrorInfo.ErrorMessage, errorInfo.ErrorMessage);
            }
            else
            {
                // TODO: Fail here
            }

            Assert.Equal(expectedResult.UserName, result.UserName);

        }

        public static IEnumerable<object[]> RegisterUserTestDataUserExistException => new List<object[]>
        {
            new object[]{ new CreateUserProfileRequest { UserName = "username2",PassKey="Dummy"},new CreateUserProfileResponse { UserName = "username2", ErrorMessage = "User already exists" } },
            new object[]{ new CreateUserProfileRequest { UserName = "UserName2", PassKey = "Dummy" }, new CreateUserProfileResponse { UserName = "UserName2", ErrorMessage = "User already exists" } },
        };
    }
}
