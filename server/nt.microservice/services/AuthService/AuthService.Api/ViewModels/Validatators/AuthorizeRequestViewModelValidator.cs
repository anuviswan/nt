using AuthService.Api.ViewModels.Validate;
using FluentValidation;

namespace AuthService.Api.ViewModels.Validatators;
public class AuthorizeRequestViewModelValidator : AbstractValidator<AuthorizeRequestViewModel>
{
    public AuthorizeRequestViewModelValidator()
    {
        RuleFor(x => x.UserName).NotEmpty()
        .NotNull()
        .MinimumLength(6)
        .WithMessage("Invalid UserName");

        RuleFor(x => x.Password).NotEmpty()
            .NotNull()
            .MinimumLength(6)
            .WithMessage("Invalid Password");
    }
}
