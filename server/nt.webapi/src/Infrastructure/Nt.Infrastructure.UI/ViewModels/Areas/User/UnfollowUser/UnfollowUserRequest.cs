using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.UnfollowUser;
public record UnfollowUserRequest
{
    [Required]
    public string UserToUnfollow { get; set; }
}
