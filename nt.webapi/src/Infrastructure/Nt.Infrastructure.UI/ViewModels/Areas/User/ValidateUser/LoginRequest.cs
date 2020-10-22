using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.User.ValidateUser
{
    public record LoginRequest
    {
        [Required]
        public string UserName { get; init; }
        [Required]
        public string PassKey { get; init; }
    }
}
