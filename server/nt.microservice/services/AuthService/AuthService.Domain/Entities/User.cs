using DapperExtensions.Mapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Domain.Entities;

public class User : IEntity
{
    public static string TABLE_NAME => "users";
    [Column("id")]
    [Key]
    public Guid Id { get; set; }

    [Column("username")]
    public string UserName { get; set; } = null!;

    [Column("password")]
    public string Password { get; set; } = null!;
}
public sealed class UserMapper : ClassMapper<User> 
{
    public UserMapper()
    {
        TableName = User.TABLE_NAME;
        Map(x => x.Id).Column("id").Key(KeyType.Assigned);
        Map(x => x.UserName).Column("username");
        Map(x => x.Password).Column("password");
        AutoMap();
    }

}