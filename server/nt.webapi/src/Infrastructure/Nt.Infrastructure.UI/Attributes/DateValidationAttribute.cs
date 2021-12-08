using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.Attributes;
public class DateValidationAttribute:ValidationAttribute 
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var memberNames = new[] { validationContext.MemberName };
        if(value is not DateTime date || date < new DateTime(1900,1,1))
        {
            return new (ErrorMessage, memberNames);
        }

        return ValidationResult.Success;
    }
}
