using System;

namespace Nt.Domain.Entities.Exceptions
{
    public class InvalidPasswordOrUsernameException : Exception
    {
        public InvalidPasswordOrUsernameException():base("Invalid Password or Username")
        {

        }
    }
}
