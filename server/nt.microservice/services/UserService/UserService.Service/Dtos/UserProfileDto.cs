using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Service.Dtos;

public record UserProfileDto
{
    public string? UserName { get; set; }
    public string? DisplayName { get; set; }

    public long TotalFollowers { get; set; }
    public bool IsFollowing { get; set; }

    public string? Bio { get; set; }
}
