using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
            public const int HttpPort = 5672;
            public const int HttpsPort = 15672;
        }
    }
    
    public static class AuthService 
    {
        public const string ServiceName = "nt-authservice-service";
        public static class Database
        {
            public const string InstanceName = "UserSqlDb";
            public const string ContainerName = "nt.authservice.db";
            public const string UserNameKey = $"{ServiceName}-UserName";
            public const string PasswordKey = $"{ServiceName}-Password";
        }
    }

    public static class MovieService
    {
        public const string ServiceName = "nt-movieservice-service";
        
        public static class Database
        {
            public const string InstanceName = "nt-movieservice-db";
            public const string ContainerName = "nt.movieservice.db";
            public const string UserNameKey = $"{ServiceName}-UserName";
            public const string PasswordKey = $"{ServiceName}-Password";
        }

        public static class  EnvironmentVariable
        {
            public const string DbUserNameKey = "MONGO_INITDB_ROOT_USERNAME";
            public const string DbPasswordKey = "MONGO_INITDB_ROOT_PASSWORD";
        }
    }
}
