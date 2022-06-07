using System.ComponentModel.DataAnnotations;

namespace nt.helper.CustomValidators;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ValidatePassword:ValidationAttribute
{
    public ValidatePassword()
    {
    }
    public override bool IsValid(object? value)
    {
        if (value is string password)
        {
            return password.Any(x => char.IsUpper(x))
                && password.Any(x => char.IsLower(x))
                && password.Length >= 6;
        }

        return base.IsValid(value);
    }
}
