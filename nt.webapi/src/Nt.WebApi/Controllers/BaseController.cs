using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Shared.Settings;

namespace Nt.WebApi.Controllers
{
    public class BaseController:ControllerBase
    {
        protected IDatabaseSettings _databaseSettings;

        protected IMapper Mapper { get; }
        public BaseController(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            Mapper = mapper;
            _databaseSettings = databaseSettings;
        }
    }
}
