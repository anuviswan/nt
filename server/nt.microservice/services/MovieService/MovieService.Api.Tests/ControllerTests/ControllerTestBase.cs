using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace MovieService.Api.Tests.ControllerTests;

public class ControllerTestsBase
{
    protected void MockModelState<TModel, TController>(TModel model, TController controller) where TController : ControllerBase
    {
        ArgumentNullException.ThrowIfNull(nameof(model));

        var validationContext = new ValidationContext(model!, null, null);
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(model!, validationContext, validationResults, true);

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
