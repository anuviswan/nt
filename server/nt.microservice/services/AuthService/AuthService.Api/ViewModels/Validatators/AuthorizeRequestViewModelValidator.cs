using AuthService.Api.ViewModels.Validate;
using FluentValidation;

namespace AuthService.Api.ViewModels.Validatators;
public class AuthorizeRequestViewModelValidator : AbstractValidator<AuthorizeRequestViewModel>
{
    public AuthorizeRequestViewModelValidator()
    {
        RuleFor(x => x.UserName).NotEmpty()
        .NotNull()
        .MinimumLength(0)
        .WithMessage("Invalid UserName or Password");

        RuleFor(x => x.Password).NotEmpty()
            .NotNull()
            .MinimumLength(0)
            .WithMessage("Invalid Password");
    }
}
