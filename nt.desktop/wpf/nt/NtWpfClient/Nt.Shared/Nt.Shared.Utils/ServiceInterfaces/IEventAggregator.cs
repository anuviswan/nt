using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Shared.Utils.ServiceInterfaces
{
    public interface IEventAggregator
    {
        void Subscribe(object subscriber);
        void Unsubscribe(object subscriber);
        void PublishMessage(object Message);
    }
}
