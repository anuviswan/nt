using FluentMigrator.Runner;

namespace AuthService.Api.Helpers.ExtensionMethods;

public static class MigrationsExtensions
{
    public static IApplicationBuilder Migrate(this IApplicationBuilder app)
    {
        try
        {
            using var scope = app.ApplicationServices.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator?.ListMigrations();
            migrator?.MigrateUp();
            
        }
        catch(Exception ex)
        {
        }
        return app;
    }
}
