using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace MovieService.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {

        public BaseController(ILogger logger)
        {
            Logger = logger;
        }
        protected ILogger Logger { get; }
    }
}
