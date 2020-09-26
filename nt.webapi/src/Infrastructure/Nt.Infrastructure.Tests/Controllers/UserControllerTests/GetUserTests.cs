using Moq;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;
using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class GetUserTests : ControllerTestBase<UserProfileEntity>
    {
        public GetUserTests(ITestOutputHelper testOutput) : base(testOutput)
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
        [MemberData(nameof(GetUserTestData))]
        public async Task GetUser(string userName, UserProfileResponse expectedOutput)
        {
            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.GetUserAsync(userName))
                .Returns(Task.FromResult(result: EntityCollection.Single(x => x.UserName.StartsWith(userName))));
            var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);
            var result = await userController.GetUser(userName);
            Assert.Equal(expectedOutput.UserName, result.UserName);
            Assert.Equal(expectedOutput.DisplayName, result.DisplayName);
            Assert.False((result as IErrorInfo).HasError);
        }
        public static IEnumerable<object[]> GetUserTestData => new List<object[]>
         {
             new object[]{"username2", new UserProfileResponse{ UserName="username2",DisplayName="User Name 2" } },
         };


        [Theory]
        [MemberData(nameof(GetUser_InvalidCasesTestData))]
        public async Task GetUser_InvalidCases(string userName)
        {
            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.GetUserAsync(userName))
                 .Throws<Exception>();
            var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);
            var result = await userController.GetUser(userName);
            Assert.True((result as IErrorInfo).HasError);
        }
        public static IEnumerable<object[]> GetUser_InvalidCasesTestData => new List<object[]>
          {
              new object[]{"username12" },
          };
    }
}
