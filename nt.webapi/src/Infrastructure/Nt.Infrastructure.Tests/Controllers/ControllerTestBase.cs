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
using Nt.Infrastructure.WebApi.Controllers;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Nt.Infrastructure.WebApi.ExtensionMethods;

namespace Nt.Infrastructure.Tests.Controllers
{
    public class ControllerTestBase<TEntityCollection> where TEntityCollection : BaseEntity, new()
    {
        protected IMapper Mapper { get; set; }
        protected ITestOutputHelper Output { get; }

        public ControllerTestBase(ITestOutputHelper output)
        {
            Output = output;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(typeof(Profile).FindAllDerivedTypes(typeof(BaseController).Assembly).Select(x=> Activator.CreateInstance(x)).Cast<Profile>());
            });

            Mapper = mappingConfig.CreateMapper();
            InitializeCollection();
        }

        protected virtual void InitializeCollection()
        {

        }
        public List<TEntityCollection> EntityCollection { get; set; }

        protected void MockModelState<TModel,TController>(TModel model, TController controller) where TController: ControllerBase
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }
    }
}
