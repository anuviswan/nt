﻿using Moq;
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
using System.Linq.Expressions;
using Xunit.Abstractions;
using Nt.Infrastructure.Tests.Helpers.CustomTraits;

namespace Nt.Infrastructure.Tests.Services.UserManagementServiceTests
{
    public class SearchTests : ServiceTestBase<UserProfileEntity>
    {
        public SearchTests(ITestOutputHelper output) : base(output)
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
        [ServiceTest(nameof(UserManagementService)), Feature]
        public async Task SearchUser(string userName, IEnumerable<string> expectedOutput)
        {
            var mockUserProfileRepository = new Mock<IUserProfileRepository>();
            mockUserProfileRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<UserProfileEntity, bool>>>()))
                                      .Returns(Task.FromResult(EntityCollection.Where(x => (x.DisplayName.ToLower().StartsWith(userName.ToLower())
                                                                                                || x.UserName.ToLower().StartsWith(userName.ToLower()))
                                                                                                && x.IsDeleted == false)));

            var mockUnitOfwork = new Mock<IUnitOfWork>();
            mockUnitOfwork.SetupGet(x => x.UserProfileRepository).Returns(mockUserProfileRepository.Object);

            var userProfileService = new UserManagementService(mockUnitOfwork.Object);
            var result = await userProfileService.SearchUserAsync(userName);
            Assert.True(expectedOutput.Count() == result.Count());
            Assert.Equal(expectedOutput, result.Select(x => x.UserName));
        }

        public static IEnumerable<object[]> SearchUserTestData => new List<object[]>
            {
                new object[]{"anu", new[] {"AnuViswan" } },
                new object[]{"Anu", new[] {"AnuViswan"} },
                new object[]{"Doesn'tExist", Enumerable.Empty<string>()},
                new object[]{string.Empty,Enumerable.Empty<string>()},
                new object[]{" ",Enumerable.Empty<string>()},
            };
    }
}
