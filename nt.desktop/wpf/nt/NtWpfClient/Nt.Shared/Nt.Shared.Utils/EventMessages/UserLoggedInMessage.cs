using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Shared.Utils.EventMessages
{
    public class UserLoggedInMessage
    {
        public UserLoggedInMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
