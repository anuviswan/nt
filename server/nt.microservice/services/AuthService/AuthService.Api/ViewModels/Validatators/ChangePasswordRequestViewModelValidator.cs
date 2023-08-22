using AuthService.Api.ViewModels.ChangePassword;
using FluentValidation;

namespace AuthService.Api.ViewModels.Validatators;

public class ChangePasswordRequestViewModelValidator : AbstractValidator<ChangePasswordRequestViewModel>
{
    public ChangePasswordRequestViewModelValidator()
    {
        RuleFor(x=>x.OldPassword).NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .WithMessage("Invalid Old Password");

        RuleFor(x => x.NewPassword).NotEmpty()
            .NotNull()
            .MinimumLength(8)
            .WithMessage("Invalid New Password"); ;
    }
}
