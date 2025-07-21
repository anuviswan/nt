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

            public static class Environement
            {
                public const string Key = "ServiceRegistrationConfig";
                public const string ServiceName = $"{Key}__serviceName";
                public const string ServiceId = $"{Key}__serviceId";
                public const string ServiceHost = $"{Key}__serviceHost";
                public const string ServicePort = $"{Key}__servicePort";
                public const string ServiceHealthCheckUrl = $"{Key}__healthCheckUrl";
                public const string RegistryUri = $"{Key}__registryUri";
                public const string DeregisterAfter = $"{Key}__deregisterAfterMinutes";
            }
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

    public static class AggregatorUserIdentityService
    {
        public const string ServiceName = "nt-useridentityaggregator-service";
        public const string ServiceMapping_RegistryUri = "ServiceMappingConfig__RegistryUri";
        public const string ServiceMappingUserServiceKey = "ServiceMappingConfig__Services__0__Key";
        public const string ServiceMappingUserServiceName = "ServiceMappingConfig__Services__0__Name";
        public const string ServiceMappingAuthServiceKey = "ServiceMappingConfig__Services__1__Key";
        public const string ServiceMappingAuthServiceName = "ServiceMappingConfig__Services__1__Name";
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

        public static class Environment
        {
            public const string RabbitMqHost = "RabbitMqSettings__uri";
            public const string RabbitMqUserName = "RabbitMqSettings__username";
            public const string RabbitMqPassword = "RabbitMqSettings__password";
        }

        public static class LoadBalancer
        {
            public const string InstanceName = "nt-authservice-loadbalancer";
        }

        public static class Sidecar
        {
            public const string InstanceName = "nt-authservice-loadbalancer-sidecar";
        }
    }


    public static class UserService
    {
        public const string ServiceName = "nt-userservice-service";

        public class Database
        {
            public const string InstanceName = "nt-userservice-db";
            public const string ContainerName = "nt.userservice.db";
            public const string PasswordKey = $"{ServiceName}-Password";
        }

        public static class Environment
        {
            public const string RabbitMqHost = "RabbitMqSettings__host";
            public const string RabbitMqUserName = "RabbitMqSettings__username";
            public const string RabbitMqPassword = "RabbitMqSettings__password";
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
            public const string DbName = "MovieDatabase__DatabaseName";
            public const string DbCollection = "MovieDatabase__MovieCollectionName";
        }
    }

    public static class ReviewService
    {
        public const string ServiceName = "nt-reviewservice-service";


        public static class Cache
        {
            public const string InstanceName = "nt-reviewservice-cache";
            public const string ContainerName = "nt.reviewservice.cache";
            public const string UserNameKey = $"{ServiceName}-UserName";
            public const string PasswordKey = $"{ServiceName}-Password";
        }

        public static class Database
        {
            public const string InstanceName = "nt-reviewservice-db";
            public const string ContainerName = "nt.reviewservice.db";
            public const string UserNameKey = $"{ServiceName}-UserName";
            public const string PasswordKey = $"{ServiceName}-Password";
        }

        public static class EnvironmentVariable
        {
            public const string DbUserNameKey = "MONGO_INITDB_ROOT_USERNAME";
            public const string DbPasswordKey = "MONGO_INITDB_ROOT_PASSWORD";
            public const string DbName = "ReviewDatabase__DatabaseName";
            public const string DbCollection = "ReviewDatabase__ReviewCollectionName";
        }
    }
}
