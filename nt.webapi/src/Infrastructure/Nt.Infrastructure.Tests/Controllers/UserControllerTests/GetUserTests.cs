using Nt.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class GetUserTests : ControllerTestBase<UserProfileEntity>
    {
        public GetUserTests(ITestOutputHelper testOutput) : base(testOutput)
        {

        }

        public async Task GetUserValidUserName()
        {

        }
    }
}
