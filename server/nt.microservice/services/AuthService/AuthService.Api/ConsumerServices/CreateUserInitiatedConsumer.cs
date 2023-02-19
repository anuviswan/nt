using AuthService.Domain.Entities;
using AuthService.Service.Command;
using MapsterMapper;
using MassTransit;
using MediatR;
using nt.shared.dto.User;

namespace AuthService.Api.ConsumerServices;

public class CreateUserInitiatedConsumer : IConsumer<CreateUserInitiated>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndPoint;

    public CreateUserInitiatedConsumer([FromServices]IMapper mapper, [FromServices] IMediator mediator, [FromServices]IPublishEndpoint publishEndPoint)
    {
        (_mapper, _mediator, _publishEndPoint) = (mapper, mediator, publishEndPoint);
    }
    public async Task Consume(ConsumeContext<CreateUserInitiated> context)
    {
        try
        {
            var userModel = _mapper.Map<User>(context.Message);
            userModel.Id = Guid.NewGuid();

            var result = await _mediator.Send(new AddUserCommand
            {
                User = userModel,
            });
        }
        catch (Exception e)
        {
            await _publishEndPoint.Publish<CreateUserInitiatedError>(new CreateUserInitiatedError()
            {
                ExceptionMessage = e.ToString(),
                UserName = context.Message.UserName
            });
        }
       

       // var response = _mapper.Map<AddUserResponseViewModel>(result);
    }
}

public class CreateUserInitiatedConsumerDefinition: ConsumerDefinition<CreateUserInitiatedConsumer>
{
    public CreateUserInitiatedConsumerDefinition()
    {
        EndpointName = $"{nameof(CreateUserInitiatedConsumer)}-{CreateUserInitiated.QueueName}";
    }
}
