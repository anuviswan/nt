using AuthService.Api.Authentication;
using AuthService.Api.ConsumerServices;
using AuthService.Api.Helpers.ExtensionMethods;
using AuthService.Api.Settings;
using AuthService.Data.Database;
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
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        logger.Debug("init main");

        var builder = WebApplication.CreateBuilder(args);
        var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMqSettings)).Get<RabbitMqSettings>();
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
        builder.Services.AddTransient<IUnitOfWorkFactory>(con => new PgUnitOfWorkFactory(connectionString));

        var serviceProvider = builder.Services.BuildServiceProvider();
        var mapperService = serviceProvider.GetService<IMapper>();
        var mediatorService = serviceProvider.GetService<IMediator>();


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
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudiences = new string[]
                    {
                builder.Configuration["Jwt:Aud1"],
                builder.Configuration["Jwt:Aud2"]
                    },
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.Migrate();
        //app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }
}