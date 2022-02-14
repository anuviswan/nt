using AuthService.Api.Bootstrap;

namespace AuthService.Api.Helpers.ExtensionMethods;
public static class IServiceCollectionExtensions
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        Validators.AddValidators(serviceCollection);
    }

    public static void AddDapperTypeMaps(this IServiceCollection serviceCollection)
    {
        DapperTypeMapping.Map();
    }
}
