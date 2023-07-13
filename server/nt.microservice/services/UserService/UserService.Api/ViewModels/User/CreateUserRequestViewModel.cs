using nt.helper.CustomValidators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UserService.Api.ViewModels.User;
public record CreateUserRequestViewModel
{
    [Required(ErrorMessage = $"{nameof(UserName)} is mandatory.")]
    [MinLength(6,ErrorMessage = $"{nameof(UserName)} should be minimum 6 characters.")]
    [MaxLength(15, ErrorMessage = $"{nameof(UserName)} should be maximum 15 characters.")]
    [JsonPropertyName("userName")]
    public string UserName { get; init; }
    
    [Required(ErrorMessage = $"{nameof(Password)} is mandatory.")]
    [MinLength(6, ErrorMessage = $"{nameof(Password)} should be minimum 6 characters long")]
    [ValidatePassword(ErrorMessage = $"{nameof(Password)} should contain atleast one upper case character and atleast one lower case character")]
    [JsonPropertyName("passKey")]
    public string Password { get; init; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("bio")]
    public string? Bio { get; set; }
}
