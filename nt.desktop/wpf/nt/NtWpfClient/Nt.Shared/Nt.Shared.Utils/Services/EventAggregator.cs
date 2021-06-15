using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Nt.Shared.Utils.ServiceInterfaces;

namespace Nt.Shared.Utils.Services
{
    public class EventAggregator : IEventAggregator
    {
        public void PublishMessage(object Message)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(object subscriber)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(object subscriber)
        {
            throw new NotImplementedException();
        }

        private class Handler
        {
            private readonly object _subscriber;
            private Dictionary<Type, MethodInfo> _handlers = new Dictionary<Type, MethodInfo>();
            public Handler(object subscriber)
            {
                _subscriber = subscriber;

                var interfaces = _subscriber.GetType()
                                           .GetTypeInfo()
                                           .ImplementedInterfaces
                                           .Where(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandle<>));

                foreach(var @interface in interfaces)
                {
                    var genericTypeArg = @interface.GetTypeInfo().GenericTypeArguments[0];
                    var method = @interface.GetRuntimeMethod("HandleAsync", new[] { genericTypeArg });

                    _handlers.Add(genericTypeArg,method);
                }
             
            }
        }
    }
}
