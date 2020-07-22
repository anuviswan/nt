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
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Services.UserProfileService
{
    public class UserProfileServiceSearchTests : ServiceTestBase<UserProfileEntity>
    {
        public UserProfileServiceSearchTests(ITestOutputHelper output):base(output)
        {

        }
        protected override void InitializeCollection()
        {
            EntityCollection = Enumerable.Range(1, 10).Select(x => new UserProfileEntity
            {
                DisplayName = $"User Name {x}",
                UserName = $"username{x}",
                PassKey = $"passKey{x}",
                Bio = $"bio{x}"
            }).ToList();
        }

        [Fact]
        public void GetUsers()
        {
            var mockUserProfileRepository = new Mock<IUserProfileRepository>();
        }

    }
}
