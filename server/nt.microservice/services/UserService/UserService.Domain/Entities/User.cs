using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

[NotMapped]
public class User : IEntity
{
    public long Id { get; set; }
    public string UserName { get; set; }
    
    public string Password { get; set; }
}
