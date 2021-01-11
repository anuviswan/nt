using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Nt.Application.Services.User;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
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
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            // Act
            var userManagementService = new UserManagementService(mockUnitOfWork.Object);
            await userManagementService.FollowUserAsync(currentUser,userToFollow);

            var followedUser = EntityCollection.Single(x => string.Equals(x.Id, userToFollow));
            // Arrange
            Assert.Contains(currentUser, followedUser.Followers);
            
        }

        public static IEnumerable<object[]> FollowUserSuccessTestData => new List<object[]>
        {
            new object[]
            {
                string.Format(Utils.MockUserIdFormat,1),
                string.Format(Utils.MockUserIdFormat,2),
            }
        };
        #endregion
    }
}
