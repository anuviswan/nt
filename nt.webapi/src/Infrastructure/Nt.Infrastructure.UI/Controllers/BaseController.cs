using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.Infrastructure.WebApi.ViewModels.Common;
using System.Runtime.CompilerServices;

namespace Nt.Infrastructure.WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IMapper Mapper { get; }

        public BaseController(IMapper mapper) => Mapper = mapper;

        protected T CreateErrorResponse<T>(params string[] errorMessages) where T : IErrorInfo, new() => CreateErrorResponse(new T(), errorMessages);

        protected T CreateErrorResponse<T>(T response,params string[] errorMessage) where T : IErrorInfo, new()
        {
            response.Errors.AddRange(errorMessage);
            return response;
        }

        protected string GetLocationString<TController>(TController controller, [CallerMemberName]string action = "") where TController:BaseController
        {
            return $"{controller.GetType().Name}.{action}";
        }
    }
}
