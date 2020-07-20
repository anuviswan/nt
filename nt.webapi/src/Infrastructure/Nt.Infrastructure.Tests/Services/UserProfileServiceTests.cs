using Moq;
using Nt.Application.Services.User;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Nt.Infrastructure.Tests.Services
{
    public class UserProfileServiceTests:ServiceTestBase<UserProfileEntity>
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
        public void GetUsers()
        {
            
        }

        [Theory]
        [MemberData(nameof(SearchUserTestData))]
        public async Task SearchUser(string userName, IEnumerable<string> expectedOutput)
        {
            var mockUserProfileRepository = new Mock<IUserProfileRepository>();
            mockUserProfileRepository.Setup(x => x.GetAsync(x=>x.DisplayName.StartsWith(userName) || x.UserName.StartsWith(userName)))
                                      .Returns(Task.FromResult(result: EntityCollection.Where(x => x.UserName.StartsWith(userName))));

            var mockUnitOfwork = new Mock<IUnitOfWork>();
            mockUnitOfwork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

            var userProfileService = new UserManagementService(mockUnitOfwork.Object);
            var result = await userProfileService.SearchUserAsync(userName);

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
