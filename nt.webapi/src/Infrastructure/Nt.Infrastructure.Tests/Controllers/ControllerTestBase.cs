using AutoMapper;
using Nt.Domain.Entities.Entities;
using Nt.Infrastructure.WebApi.ExtensionMethods;
using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.Tests.Controllers;
public class ControllerTestBase<TEntityCollection> where TEntityCollection : BaseEntity, new()
{
    protected IMapper Mapper { get; set; }
    protected ITestOutputHelper Output { get; }

    public ControllerTestBase(ITestOutputHelper output)
    {
        Output = output;
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfiles(typeof(Profile)
                .FindAllDerivedTypes(typeof(BaseController).Assembly)
                .Select(x=> Activator.CreateInstance(x))
                .Cast<Profile>());
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
