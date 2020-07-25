using Nt.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.Authentication
{
    public interface ITokenGenerator
    {
        string Generate(UserProfileEntity userProfile);
    }
}
