using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.User;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using Nt.Domain.ServiceContracts.User;
using Nt.Infrastructure.Tests.Helpers;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;
using Nt.Infrastructure.Tests.Helpers.TestData;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.UserManagementServiceTests
{
    public class FollowUserTests : ServiceTestBase<UserProfileEntity>
    {
        public FollowUserTests(ITestOutputHelper output) : base(output)
        {
        }
        protected override void InitializeCollection()
        {
            Output.WriteLine("Initialized"); //TODO: Fix this removing this line causes InitializeCollection not being called randomly
            EntityCollection = new List<UserProfileEntity>(MovieReviewCollectionHelper.UserCollection);
        }


        #region Valid Cases 
        [Theory]
        [MemberData(nameof(FollowUserSuccessTestData))]
        [ServiceTest(nameof(UserManagementService)), Feature]
        public async Task FollowUserSuccess(string currentUser,string userToFollow)
        {
            // Arrange
            var mockUserRepository = new Mock<IUserProfileRepository>();
            mockUserRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                               .Returns((Expression<Func<UserProfileEntity, bool>> x) =>
                               Task.FromResult(EntityCollection.AsQueryable<UserProfileEntity>().Where(x).AsEnumerable()));

            mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<UserProfileEntity>()))
                .Callback((UserProfileEntity user) =>
                {
                    var userToUpdate = EntityCollection.Single(x => string.Equals(x.UserName, user.UserName));
                    userToUpdate = userToUpdate with { Followers = user.Followers };
                });

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserRepository.Object);

            // Act
            var userManagementService = new UserManagementService(mockUnitOfWork.Object);
            await userManagementService.FollowUserAsync(currentUser,userToFollow);

            var followedUser = EntityCollection.Single(x => string.Equals(x.UserName, userToFollow));
            // Arrange
            Assert.Contains(currentUser, followedUser.Followers);
            
        }

        public static IEnumerable<object[]> FollowUserSuccessTestData => new List<object[]>
        {
            new object[]
            {
                "UserName 1",
                "UserName 2",
            }
        };
        #endregion
    }
}
