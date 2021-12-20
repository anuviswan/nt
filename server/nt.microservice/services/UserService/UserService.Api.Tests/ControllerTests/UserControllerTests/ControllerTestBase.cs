﻿using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UserService.Api.Tests.ControllerTests.UserControllerTests;
public class ControllerTestBase
{
    protected void MockModelState<TModel, TController>(TModel model, TController controller) where TController : ControllerBase
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