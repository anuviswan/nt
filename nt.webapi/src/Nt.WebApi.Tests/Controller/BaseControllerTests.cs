using Moq;
using Nt.WebApi.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.WebApi.Tests.Controller
{
    public class BaseControllerTests<TEntityCollection> where TEntityCollection : BaseEntity, new()
    {
        protected List<TEntityCollection> EntityCollection { get; set; }
    }
}
