﻿using Moq;
using Nt.Domain.Entities.User;
using Nt.Domain.ServiceContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Nt.Infrastructure.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.GetAllUser;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class SearchUserTests : ControllerTestBase<UserProfileEntity>
    {
        public SearchUserTests(ITestOutputHelper testOutput) : base(testOutput)
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

        #region ResponseStatus 200

        [Theory]
        [MemberData(nameof(SearchUser_ResponseStatus_200_TestData))]
        public async Task SearchUser_ResponseStatus_200(string userName, IEnumerable<string> expectedOutput)
        {
            // Arrange
            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.SearchUserAsync(userName))
                .Returns(Task.FromResult(result: EntityCollection.Where(x => x.UserName.StartsWith(userName))));
            var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);

            // Act
            var response = await userController.SearchUser(userName);

            // Assert
            var okResponse = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsAssignableFrom<IEnumerable<UserProfileResponse>>(okResponse.Value);
            Assert.Equal(expectedOutput.Count(), result.Count());
            Assert.Equal(expectedOutput, result.Select(x => x.UserName));
        }

        public static IEnumerable<object[]> SearchUser_ResponseStatus_200_TestData => new List<object[]>
    {
        new object[]{"username2",new List<string>{"username2"} },
        new object[]{"username1",new List<string>{"username1", "username10" } },
        new object[]{"user", Enumerable.Range(1, 10).Select(x=>$"username{x}") },
        new object[]{"doesn'texist", Enumerable.Empty<string>()},
    };

        #endregion
    }
}
