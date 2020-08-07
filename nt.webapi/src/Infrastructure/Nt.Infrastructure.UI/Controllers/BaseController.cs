using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.Infrastructure.WebApi.ViewModels.Common;

namespace Nt.Infrastructure.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IMapper Mapper { get; }
        public BaseController(IMapper mapper) => Mapper = mapper;

        protected T CreateErrorResponse<T>(string errorMessage) where T:IErrorInfo,new() => new T { ErrorMessage = errorMessage };

    }
}
