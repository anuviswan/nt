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

namespace Nt.Infrastructure.Tests.Controllers
{
    public class UserControllerTests:ControllerTestBase<UserProfileEntity>
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
            var mockRepository = new Mock<IUserManagementService>();
            mockRepository.Setup(x => x.GetAllUsersAsync()).Returns(Task.FromResult(EntityCollection.AsEnumerable()));
            var userController = new UserController(Mapper, null,mockRepository.Object);

            var result = (await userController.GetAll()).ToList();
            Assert.True(result.Count == 10);
        }

    }
}
