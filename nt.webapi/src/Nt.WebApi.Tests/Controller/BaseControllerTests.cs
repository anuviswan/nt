using AutoMapper;
using Moq;
using Nt.WebApi.Profiles;
using Nt.WebApi.Shared.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.WebApi.Tests.Controller
{
    public class BaseControllerTests<TEntityCollection> where TEntityCollection : BaseEntity, new()
    {
        protected IMapper Mapper { get; set; }
        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MovieEntityProfile());
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
