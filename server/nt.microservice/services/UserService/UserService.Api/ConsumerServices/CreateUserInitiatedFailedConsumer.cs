using MassTransit;
using nt.shared.dto.User;

namespace UserService.Api.ConsumerServices;

public class CreateUserInitiatedFailedConsumer : IConsumer<CreateUserInitiatedFailed>
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    public CreateUserInitiatedFailedConsumer([FromServices] IMediator mediator, [FromServices] ILogger<CreateUserInitiatedFailedConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<CreateUserInitiatedFailed> context)
    {
        var username = context.Message.UserName;
        var exceptionMessage = context.Message.ExceptionMessage;


        _logger.LogError($"User [{username}] Created Failed with Message {exceptionMessage}");

        var result = await _mediator.Send(new RemoveUserCommand
        {
            UserName = username,
        });

        if( result != null )
        {
            _logger.LogInformation($"User {result.UserName} has been removed successfully");
        }
    }
}
