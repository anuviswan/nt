namespace AuthService.Domain.Entities;
public class User : IEntity
{
    public static string TABLE_NAME => "Users";
    public Guid Id { get; set; }
    public string UserName { get; set; }

    public string Password { get; set; }
}
