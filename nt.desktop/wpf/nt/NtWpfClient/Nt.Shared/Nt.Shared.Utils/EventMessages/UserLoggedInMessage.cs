using System;
using System.Collections.Generic;
using System.Text;
using Nt.Shared.Utils.Interfaces;

namespace Nt.Shared.Utils.EventMessages
{
    public class UserLoggedInMessage:IEventMessage
    {
        public UserLoggedInMessage(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
