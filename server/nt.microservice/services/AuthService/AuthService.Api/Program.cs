using AuthService.Api.Authentication;
using AuthService.Api.ConsumerServices;
using AuthService.Api.Helpers.ExtensionMethods;
using AuthService.Api.Settings;
using AuthService.Data.Database;
using AuthService.Data.Interfaces.Repository;
using AuthService.Data.Repository;
using AuthService.Service.Query;
using FluentMigrator.Runner;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using Npgsql.Logging;
using Prometheus;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        logger.Debug("init main");

        var builder = WebApplication.CreateBuilder(args);
        var rabbitMqSettings = builder.Configuration
                                      .GetSection(nameof(RabbitMqSettings))
                                      .Get<RabbitMqSettings>();
        if ((rabbitMqSettings?.Validate()) != true)
        {
            throw new Exception("Unable to read RabbitMq Settings");
        }
        // Add services to the container.
        builder.Services.AddDapperTypeMaps();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var config = TypeAdapterConfig.GlobalSettings;
        builder.Services.AddSingleton(config);
        builder.Services.AddScoped<IMapper, ServiceMapper>();


        builder.Services.AddSingleton<ITokenGenerator, JwtTokenGenerator>();
        builder.Services.AddMediatR(typeof(ValidateUserQuery).Assembly);
        builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        var connectionString = builder.Configuration.GetConnectionString("UserSqlDb");
        builder.Services.AddTransient<IUnitOfWorkFactory>(con => new PgUnitOfWorkFactory(connectionString??string.Empty));

        builder.Services.AddMassTransit(mt =>
                                {
                                    mt.AddConsumersFromNamespaceContaining(typeof(CreateUserInitiatedConsumer));

                                    mt.UsingRabbitMq((cntxt, cfg) =>
                                    {
                                        cfg.Host(rabbitMqSettings.Uri, "/", c =>
                                        {
                                            c.Username(rabbitMqSettings.UserName);
                                            c.Password(rabbitMqSettings.Password);
                                        });

                                        cfg.ConfigureEndpoints(cntxt);

                                    });
                                  });

        builder.Services.AddFluentValidation();
        builder.Services.AddValidators();

        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
        builder.Host.UseNLog();

        builder.Services//.AddLogging(x => x.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(x=>x.AddPostgres()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(AuthService.Data.Migrations.InitialSeed202302072003).Assembly).For.Migrations());


        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

                if ((jwt?.Validate()) != true)
                    throw new Exception("Unable to read Jwt Settings");

                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudiences =
                    [
                        jwt.Aud1,
                        jwt.Aud2,
                        jwt.Aud3,
                    ],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                };
            });

        builder.Services.AddAuthorization();
        NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Trace, true, true);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseMetricServer();
        app.UseHttpMetrics();

        app.Migrate();
        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}