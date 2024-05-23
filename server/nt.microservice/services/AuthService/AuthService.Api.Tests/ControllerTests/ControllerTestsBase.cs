using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AuthService.Api.Tests.ControllerTests;
public class ControllerTestsBase
{
    protected void MockModelState<TModel, TController>(TModel model, TController controller) where TController : ControllerBase
    {
        var validationContext = new ValidationContext(model, null, null);
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(model, validationContext, validationResults, true);

        foreach (var validationResult in validationResults)
        {
            controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
        }
    }

    protected ILogger<T> CreateNullLogger<T>()
    {
        var loggerFactory = new NullLoggerFactory();
        return loggerFactory.CreateLogger<T>();
    }
}
