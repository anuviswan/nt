using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Nt.Infrastructure.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IMapper Mapper { get; }
        public BaseController(IMapper mapper) => Mapper = mapper;
    }
}
