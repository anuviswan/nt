using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Data.Interfaces.Entities;

public class UserFollower : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [ForeignKey(nameof(Follower))]
    public long FollowerId { get; set; }
    public virtual UserMetaInformation Follower { get; set; } = null!;

    [ForeignKey(nameof(Followee))]
    public long FolloweeId { get; set; }
    public virtual UserMetaInformation Followee { get; set; } = null!;

}

