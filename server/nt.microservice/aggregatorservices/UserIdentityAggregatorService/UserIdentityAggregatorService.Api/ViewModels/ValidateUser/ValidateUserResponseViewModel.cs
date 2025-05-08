namespace UserIdentityAggregatorService.Api.ViewModels.ValidateUser;


public record ValidateUserResponseViewModel
{
    public required string Token { get; set; }

    public required bool IsAuthenticated { get; set; }

    public required DateTime LoginTime { get; set; }

    public required string UserName { get; set; }

    public string? DisplayName { get; set; }

    public string? Bio { get; set; }
}
