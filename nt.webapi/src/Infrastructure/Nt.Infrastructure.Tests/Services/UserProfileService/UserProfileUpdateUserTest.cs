using Moq;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.RepositoryContracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.UserProfileService
{
    public class UserProfileUpdateUserTest : ServiceTestBase<UserProfileEntity>
    {
        public UserProfileUpdateUserTest(ITestOutputHelper output) : base(output)
        {
        }

        protected override void InitializeCollection()
        {
            var random = new Random();
            EntityCollection = new()
            {
                new UserProfileEntity { UserName = "AnuViswan", DisplayName = "Anu Viswan", IsDeleted = false },
                new UserProfileEntity { UserName = "ManuViswan", DisplayName = "Manu Viswan", IsDeleted = false },
                new UserProfileEntity { UserName = "AnuViswan", DisplayName = "AnuViswan", IsDeleted = true },
            };
        }

        [Theory]
        [MemberData(nameof(UpdateUserTestFailureTestData))]
        public async Task UpdateUserTestFailure(UserProfileEntity request,bool expectedResult)
        {
            var mockUserProfileRepository = new Mock<IUserProfileRepository>();
            mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => x.UserName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted)));

            mockUserProfileRepository.Setup(x => x.UpdateAsync(It.IsAny<UserProfileEntity>()))
                .Returns(Task.FromResult(true));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

            var userProfileService = new Nt.Application.Services.User.UserProfileService(mockUnitOfWork.Object);
            var result = await userProfileService.UpdateUserAsync(request);

            Assert.Equal(expectedResult, result);
            mockUserProfileRepository.Verify(x => x.UpdateAsync(It.IsAny<UserProfileEntity>()), Times.Never);

        }

        public static IEnumerable<object[]> UpdateUserTestFailureTestData => new[]
        {
            new object[]{new UserProfileEntity{UserName="anuviswan1",Bio="Bio"},false },
            new object[]{new UserProfileEntity{UserName="AnuViswan2",Bio="Bio"},false }
        };
        
    }
}
