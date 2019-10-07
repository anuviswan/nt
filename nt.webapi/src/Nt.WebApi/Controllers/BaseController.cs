using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nt.WebApi.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Controllers
{
    public class BaseController:ControllerBase
    {
        private IMapper _mapper;
        protected IDatabaseSettings _databaseSettings;

        protected IMapper Mapper => _mapper;
        public BaseController(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
        }
    }
}
