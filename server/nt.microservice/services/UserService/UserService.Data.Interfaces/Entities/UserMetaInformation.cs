﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Data.Interfaces.Entities;
public class UserMetaInformation : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string UserName { get; set; } = null!;
    public string? DisplayName { get; set; }
    public string? Bio { get; set; }

    public ICollection<UserFollower> Followers { get; set; } = [];
    public ICollection<UserFollower> Following { get; set; } = [];

}
