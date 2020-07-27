using Nt.Domain.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Nt.Infrastructure.WebApi.Profiles;
using Xunit.Abstractions;

namespace Nt.Infrastructure.Tests.Controllers
{
    public class ControllerTestBase<TEntityCollection> where TEntityCollection : BaseEntity, new()
    {
        protected IMapper Mapper { get; set; }
        protected ITestOutputHelper Output { get; }

        public ControllerTestBase(ITestOutputHelper output)
        {
            Output = output;1;
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
        public List<TEntityCollection> EntityCollection { get; set; }
    }
}
