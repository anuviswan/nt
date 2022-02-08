using AuthService.Api.ViewModels.AddUser;
using FluentValidation;

namespace AuthService.Api.ViewModels.Validatators;
public class AddUserRequestViewModelValidator : AbstractValidator<AddUserRequestViewModel>
{
    public AddUserRequestViewModelValidator()
    {
        RuleFor(x => x.UserName).NotEmpty()
        .NotNull()
        .MinimumLength(6)
        .MaximumLength(30)
        .WithMessage("Username should be between 6-30 characters");

        RuleFor(x => x.Password).NotEmpty()
            .NotNull()
            .MinimumLength(6)
            .MaximumLength(30)
            .WithMessage("Password should be between 6-30 characters");
    }
}
