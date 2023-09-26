using Nt.Saga.Orchestrator.Services.AuthService;
using Nt.Saga.Orchestrator.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<AuthService>(options =>
{
    options.BaseAddress = new Uri("http://host.docker.internal:8100/");
});

builder.Services.AddHttpClient<UserService>(options =>
{
    options.BaseAddress = new Uri("http://host.docker.internal:8301/");
});

//builder.Services.AddScoped<AuthService, AuthService>();
//builder.Services.AddScoped<UserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
