using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

public class Follower : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public long UserId { get; set; }
    public long FollowerId { get; set;}

}
