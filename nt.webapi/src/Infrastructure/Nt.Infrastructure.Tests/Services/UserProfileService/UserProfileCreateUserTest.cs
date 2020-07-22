using Moq;
using Nt.Application.Services.User;
using Nt.Domain.Entities.User;
using Nt.Domain.RepositoryContracts;
using Nt.Infrastructure.WebApi.Profiles;
using Nt.Infrastructure.WebApi.ViewModels.Areas.User.RequestObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Domain.RepositoryContracts.User;
using Xunit;
using Nt.Domain.Entities.Exceptions;
using System.Linq.Expressions;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.UserProfileService
{
    public class UserProfileCreateUserTest:ServiceTestBase<UserProfileEntity>
    {
        public UserProfileCreateUserTest(ITestOutputHelper output):base(output)
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
        [MemberData(nameof(CreateUserTestSuccessTestCases))]
        public async Task CreateUserTestSuccess(UserProfileEntity request, UserProfileEntity response)
        {
            var mockUserProfileRepository = new Mock<IUserProfileRepository>();
            mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => x.UserName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted)));
            mockUserProfileRepository.Setup(x => x.CreateAsync(request))
                .Returns((UserProfileEntity req)=>Task.FromResult(EntityCollection.Any(x=>x.UserName.Equals(req.UserName))?throw new Exception():request));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);
            
            var userProfileService = new Nt.Application.Services.User.UserProfileService(mockUnitOfWork.Object);
            var result = await userProfileService.CreateUserAsync(request);

            Assert.Equal(response.UserName, result.UserName);
            mockUserProfileRepository.Verify(mock => mock.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()), Times.Once());
            mockUserProfileRepository.Verify(mock => mock.CreateAsync(It.IsAny<UserProfileEntity>()), Times.Once());
        }

        public static IEnumerable<object[]> CreateUserTestSuccessTestCases => new List<object[]>
        {
            new object[]{new UserProfileEntity{UserName="anuv",DisplayName = "Display Name"}, new UserProfileEntity{UserName="anuv",DisplayName = "Display Name"} }
        };

        [Theory]
        [MemberData(nameof(CreateUserTestExceptionTestCases))]
        public async Task CreateUserTestException(UserProfileEntity request, UserProfileEntity response)
        {
            var mockUserProfileRepository = new Mock<IUserProfileRepository>();

            mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                .Returns(Task.FromResult(EntityCollection.Where(x => x.UserName.Equals(request.UserName, StringComparison.InvariantCultureIgnoreCase) && !x.IsDeleted)));
            mockUserProfileRepository.Setup(x => x.CreateAsync(request))
                .Returns((UserProfileEntity req) => Task.FromResult(request));

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

            var userProfileService = new Nt.Application.Services.User.UserProfileService(mockUnitOfWork.Object);
            await Assert.ThrowsAsync<UserNameExistsException>(()=> userProfileService.CreateUserAsync(request));
            mockUserProfileRepository.Verify(mock => mock.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()), Times.Once());
            mockUserProfileRepository.Verify(mock => mock.CreateAsync(It.IsAny<UserProfileEntity>()), Times.Never());

        }

        public static IEnumerable<object[]> CreateUserTestExceptionTestCases => new List<object[]>
        {
            new object[]{new UserProfileEntity{UserName="anuviswan",DisplayName = "Display Name"}, new UserProfileEntity{UserName="anuviswan",DisplayName = "Display Name"} }
        };
    }
}
