using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.FollowUser;
public record FollowUserRequest
{
    [Required]
    public string UserToFollow { get; set; }
}
