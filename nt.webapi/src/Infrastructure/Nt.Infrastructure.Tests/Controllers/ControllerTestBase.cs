﻿using Nt.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Nt.Infrastructure.WebApi.Profiles;

namespace Nt.Infrastructure.Tests.Controllers
{
    public class ControllerTestBase<TEntityCollection> where TEntityCollection : BaseEntity, new()
    {
        protected IMapper Mapper { get; set; }

        public ControllerTestBase()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserEntityProfile());
            });

            Mapper = mappingConfig.CreateMapper();
            InitializeCollection();
        }
        
        protected virtual void InitializeCollection()
        {

        }
        protected List<TEntityCollection> EntityCollection { get; set; }
    }
}