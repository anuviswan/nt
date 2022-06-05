using Microsoft.AspNetCore.Mvc;

namespace UserService.Api.Controllers
{
    public abstract class BaseController : Controller
    {

        public BaseController(IMediator mediator, IMapper mapper, ILogger logger)
        {
            (Mediator, Mapper, Logger) = (mediator,mapper, logger);
        }

        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }

        protected ILogger Logger { get; }
    }
}
