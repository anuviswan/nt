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

    public CreateUserInitiatedConsumer([FromServices]IMapper mapper, [FromServices] IMediator mediator)
    {
        (_mapper, _mediator) = (mapper, mediator);
    }
    public async Task Consume(ConsumeContext<CreateUserInitiated> context)
    {
        var userModel = _mapper.Map<User>(context.Message);
        userModel.Id = Guid.NewGuid();

        var result = await _mediator.Send(new AddUserCommand
        {
            User = userModel,
        });

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
