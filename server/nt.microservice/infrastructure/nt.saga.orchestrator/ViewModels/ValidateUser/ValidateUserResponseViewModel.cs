using System.Text.Json.Serialization;

namespace nt.saga.orchestrator.ViewModels.ValidateUser;

public record ValidateUserResponseViewModel
{
    public string Token { get; set; }

    public bool IsAuthenticated { get; set; }

    public DateTime LoginTime { get; set; }

    public string UserName { get; set; }

    public string DisplayName { get; set; }

    public string Bio { get; set; }
}
