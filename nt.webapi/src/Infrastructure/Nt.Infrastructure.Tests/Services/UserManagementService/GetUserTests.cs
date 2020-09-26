using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.User;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using Nt.Domain.ServiceContracts.User;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.UserManagementServiceTests
{
    public class GetUserTests : ServiceTestBase<UserProfileEntity>
    {
        public GetUserTests(ITestOutputHelper output) : base(output)
        {
        }
        protected override void InitializeCollection()
        {
            Output.WriteLine("Initialized"); //TODO: Fix this removing this line causes InitializeCollection not being called randomly
            EntityCollection = new()
            {
                new UserProfileEntity { UserName = "AnuViswan", DisplayName = "Anu Viswan", IsDeleted = false },
                new UserProfileEntity { UserName = "ManuViswan", DisplayName = "Manu Viswan", IsDeleted = false },
                new UserProfileEntity { UserName = "AnuViswan", DisplayName = "AnuViswan", IsDeleted = true },
            };
        }
        [Theory]
        [MemberData(nameof(SearchUserTestData))]
        public async Task GetUser(string userName, UserProfileEntity expectedOutput)
        {
            var mockUserProfileRepository = new Mock<IUserProfileRepository>();
            mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                      .Returns(Task.FromResult(EntityCollection.Where(x => (x.DisplayName.ToLower().StartsWith(userName.ToLower())
                                                                                                || x.UserName.ToLower().StartsWith(userName.ToLower()))
                                                                                                && x.IsDeleted == false)));
            var mockUnitOfwork = new Mock<IUnitOfWork>();
            mockUnitOfwork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);
            var userManagementService = new UserManagementService(mockUnitOfwork.Object);
            var result = await userManagementService.GetUserAsync(userName);
            Assert.Equal(expectedOutput.UserName, result.UserName);
        }
        public static IEnumerable<object[]> SearchUserTestData => new List<object[]>
         {
             new object[]{"AnuViswan", new UserProfileEntity{UserName="AnuViswan" } },
        
         };


        [Theory]
        [MemberData(nameof(SearchUserInvalidCaseTestData))]
        public async Task GetUser_InvalidCase(string userName)
        {
            var mockUserProfileRepository = new Mock<IUserProfileRepository>();
            mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                      .Returns(Task.FromResult(EntityCollection.Where(x => (x.DisplayName.ToLower().StartsWith(userName.ToLower())
                                                                                                || x.UserName.ToLower().StartsWith(userName.ToLower()))
                                                                                                && x.IsDeleted == false)));
            var mockUnitOfwork = new Mock<IUnitOfWork>();
            mockUnitOfwork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);
            var userManagementService = new UserManagementService(mockUnitOfwork.Object);
            await Assert.ThrowsAnyAsync<Exception>(() => userManagementService.GetUserAsync(userName));
        }
        public static IEnumerable<object[]> SearchUserInvalidCaseTestData => new List<object[]>
         {
             new object[]{"Anu" },

         };
    }
}
