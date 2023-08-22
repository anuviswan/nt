using AuthService.Api.ViewModels.AddUser;
using AuthService.Api.ViewModels.ChangePassword;
using AuthService.Api.ViewModels.Validatators;
using AuthService.Api.ViewModels.Validate;
using FluentValidation;

namespace AuthService.Api.Bootstrap;
public static class Validators
{
    public static void AddValidators(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IValidator<AuthorizeRequestViewModel>, AuthorizeRequestViewModelValidator>();
        serviceCollection.AddTransient<IValidator<AddUserRequestViewModel>, AddUserRequestViewModelValidator>();
        serviceCollection.AddTransient<IValidator<ChangePasswordRequestViewModel>, ChangePasswordRequestViewModelValidator>();
    }
}
