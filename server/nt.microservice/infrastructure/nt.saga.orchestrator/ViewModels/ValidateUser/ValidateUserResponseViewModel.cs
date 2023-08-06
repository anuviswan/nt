namespace nt.saga.orchestrator.ViewModels.ValidateUser;

public record ValidateUserResponseViewModel(string token,string userName,DateTime loginTime, string displayName, string bio);
