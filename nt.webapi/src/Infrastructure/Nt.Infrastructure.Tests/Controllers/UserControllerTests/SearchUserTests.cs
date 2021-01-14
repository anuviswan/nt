using Moq;
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
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Nt.Infrastructure.Tests.Helpers;

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
            EntityCollection = new List<UserProfileEntity>(MockDataHelper.UserCollection);
        }

        #region ResponseStatus 200

        [Theory]
        [MemberData(nameof(SearchUser_ResponseStatus_200_TestData))]
        [ControllerTest(nameof(UserController)), Feature]
        public async Task SearchUser_ResponseStatus_200(string userName, IEnumerable<UserProfileEntity> expectedOutput)
        {
            // Arrange
            var mockUserManagementService = new Mock<IUserManagementService>();
            mockUserManagementService.Setup(x => x.SearchUserAsync(userName))
                .Returns(Task.FromResult(result: EntityCollection.Where(x => x.UserName.StartsWith(userName,StringComparison.OrdinalIgnoreCase))));
            var userController = new UserController(Mapper, null, mockUserManagementService.Object, null);

            // Act
            var response = await userController.SearchUser(userName);

            // Assert
            var okResponse = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsAssignableFrom<IEnumerable<UserProfileResponse>>(okResponse.Value);
            Assert.Equal(expectedOutput.Count(), result.Count());
            
            foreach(var (expected,actual) in expectedOutput.OrderBy(x=>x.UserName).Zip(result.OrderBy(c=>c.UserName)))
            {
                Assert.Equal(expected.UserName, actual.UserName);
                Assert.Equal(expected.DisplayName, actual.DisplayName);
                Assert.Equal(expected.Followers.OrderBy(x=>x), actual.Followers.OrderBy(x=>x));
                Assert.Equal(expected.Bio, actual.Bio);
            }
        }

        public static IEnumerable<object[]> SearchUser_ResponseStatus_200_TestData => new List<object[]>
        {
            new object[]
            {
                 $"{nameof(UserProfileEntity.UserName)} 2",
                 new List<UserProfileEntity>
                 {
                     new UserProfileEntity
                     {
                        Id = Utils.GenerateUserIdString(2),
                        UserName = $"{nameof(UserProfileEntity.UserName)} 2",
                        DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 2",
                        Bio = $"{nameof(UserProfileEntity.Bio)} 2",
                        IsDeleted = false,
                        Followers = new[]
                        {
                            "UserName 3"
                        }
                     }
                 }
            },
            new object[]
            {
                 $"{nameof(UserProfileEntity.UserName)} 1",
                 new List<UserProfileEntity>
                 {
                     new UserProfileEntity
                     {
                        Id = string.Format(Utils.MockUserIdFormat, 1),
                        UserName = $"{nameof(UserProfileEntity.UserName)} 1",
                        DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 1",
                        Bio = $"{nameof(UserProfileEntity.Bio)} 1",
                        IsDeleted = false,
                        Followers = new[]
                        {
                            "UserName 3"
                        }
                     },
                     new UserProfileEntity
                     {
                        Id = string.Format(Utils.MockUserIdFormat, 10),
                        UserName = $"{nameof(UserProfileEntity.UserName)} 10",
                        DisplayName = $"{nameof(UserProfileEntity.DisplayName)} 10",
                        Bio = $"{nameof(UserProfileEntity.Bio)} 10",
                        IsDeleted = false,
                        Followers = Enumerable.Empty<string>()
                     }
                 }
            },

            new object[]
            {
                "user", 
                MockDataHelper.UserCollection
            },
            new object[]
            {
                "doesn'texist", 
                Enumerable.Empty<UserProfileEntity>()
            },
        };

        #endregion
    }
}
