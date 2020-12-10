using Microsoft.AspNetCore.Mvc;
using Moq;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.WebApi.Controllers;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class GetAllTests : ControllerTestBase<UserProfileEntity>
    {
        public GetAllTests(ITestOutputHelper output) : base(output)
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

        #region Status Code 200
        [Fact]
        [ControllerTest(nameof(UserController)), Feature]
        public async Task GetAll_ResponseStatus_200()
        {
            // Arrange
            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.GetAllUsersAsync()).Returns(Task.FromResult(EntityCollection.AsEnumerable()));

            var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);

            // ACtion
            var response = await userController.GetAll();

            // Assert
            var okResponse = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsAssignableFrom<IEnumerable<UserProfileResponse>>(okResponse.Value);
            Assert.Equal(EntityCollection.Count, result.Count());
        }

        #endregion


    }
}
