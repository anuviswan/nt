using AutoMapper;
using NUnit.Framework;
using System.Collections.Generic;
using Nt.Domain.Entities.Entities;
using Nt.Infrastructure.WebApi.Profiles;

namespace Nt.Infrastructure.Tests.Controller
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
