using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Nt.Infrastructure.WebApi.Controllers;
using Xunit;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.ResponseObjects;
using Xunit.Extensions;
using MongoDB.Driver;

namespace Nt.Infrastructure.Tests.Controllers
{
    public class UserControllerTests : ControllerTestBase<UserProfileEntity>
    {
        protected override void InitializeCollection()
        {
            EntityCollection = Enumerable.Range(1, 10).Select(x => new UserProfileEntity
            {
                DisplayName = $"User Name {x}",
                UserName = $"username{x}",
                PassKey = $"passKey{x}"
            }).ToList();
        }

        [Fact]
        public async Task GetAll()
        {
            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.GetAllUsersAsync()).Returns(Task.FromResult(EntityCollection.AsEnumerable()));
            var userController = new UserController(Mapper, null, mockUserManagementService.Object);

            var result = (await userController.GetAll()).ToList();
            Assert.True(result.Count == 10);
        }

        [Theory]
        [MemberData(nameof(SearchUserTestData))]
        public async Task SearchUser(string userName, IEnumerable<string> expectedOutput)
        {
            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.SearchUserAsync(userName))
                .Returns(Task.FromResult(result: EntityCollection.Where(x => x.UserName.StartsWith(userName))));

            var userController = new UserController(Mapper, null, mockUserManagementService.Object);
            var result = await userController.SearchUser(userName);

            Assert.True(expectedOutput.Count() == result.Count());
            Assert.Equal(expectedOutput, result.Select(x => x.UserName));
        }

        public static IEnumerable<object[]> SearchUserTestData => new List<object[]>
        {
            new object[]{"username2",new List<string>{"username2"} },
            new object[]{"username1",new List<string>{"username1", "username10" } },
            new object[]{"user", Enumerable.Range(1, 10).Select(x=>$"username{x}") },
            new object[]{"doesn'texist", Enumerable.Empty<string>()},
        };
    }
}
