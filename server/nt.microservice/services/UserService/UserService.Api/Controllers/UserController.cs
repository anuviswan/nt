namespace UserService.Api.Controllers;
public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public UserController(IMediator mediator,IMapper mapper)
    {
        (_mediator,_mapper) = (mediator,mapper);
    }

}
