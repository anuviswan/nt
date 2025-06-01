using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nt.helper;

public static class Constants
{
    public static class Global
    {
        public static class EnvironmentVariables
        {
            public const string RunningWithVariable = "RUNNING_WITH";
            public const string RunningWithValue = "ASPIRE";
            public const string HostVariable = "Host";
            public const string HostValue = "localhost";
        }

        public static class Common
        {
            public const string LaunchProfile = "Aspire";
        }
    }

    public static class  Gateway
    {
        public static string LaunchProfile = Global.Common.LaunchProfile;
        public static string ServiceName = "nt-gateway-service";
    }

    public static class Infrastructure
    {
        public static class Consul
        {
            public const string ServiceName = "nt-common-servicediscovery";
            public const string ContainerName = "nt.common.servicediscovery";
            public const int Port = 8500;
            public const int HttpPort = 9500;
        }

        public static class AggregatorUserIdentityService
        {
            public const string ServiceName = "nt-useridentityaggregator-service";
        }

        public static class RabbitMq
        {
            public const string ServiceName = "nt-common-rabbitmq";
            public const string ContainerName = "nt.common.rabbitmq";
            public const string UserNameKey = $"{ServiceName}-UserName";
            public const string PasswordKey = $"{ServiceName}-Password";
            public const int Port = 5672;
            public const int HttpPort = 15672;
        }
    }
}
