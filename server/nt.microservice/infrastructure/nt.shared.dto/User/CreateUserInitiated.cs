namespace nt.shared.dto.User;

public class CreateUserInitiated : IBaseEvent
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public static string QueueName => "create-user-initiated";
}
