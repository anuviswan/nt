using MassTransit;
using nt.shared.dto.User;

namespace UserService.Api.ConsumerServices;

public class CreateUserInitiatedSucceededConsumer : IConsumer<CreateUserInitiatedSucceeded>
{
    private readonly ILogger _logger;
    public CreateUserInitiatedSucceededConsumer([FromServices]ILogger<CreateUserInitiatedSucceededConsumer> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<CreateUserInitiatedSucceeded> context)
    {
        var userName = context.Message.UserName;
        _logger.LogInformation($"User {userName} created successfully");
        return Task.CompletedTask;
    }
}
