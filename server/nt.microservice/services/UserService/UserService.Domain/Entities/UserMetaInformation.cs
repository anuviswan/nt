using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;
public class UserMetaInformation:IEntity
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Bio { get; set; }
    
}
