using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

public class UserFollower : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    public long FollowerId { get; set; }
    public UserMetaInformation Follower { get; set; } = null!;
    
    public long FolloweeId { get; set; }
    public UserMetaInformation Followee { get; set; } = null !;

}
