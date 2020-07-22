using Nt.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers.UserControllerTests
{
    public class RegisterUserTests : ControllerTestBase<UserProfileEntity>
    {
        public RegisterUserTests(ITestOutputHelper testOutput) : base(testOutput)
        {

        }
    }
}
