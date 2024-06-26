﻿using UserService.Service.Dtos;

namespace UserService.Service.Command;

public class UploadProfileImageCommand:IRequest<ProfileImageDto>
{
    public required string UserName { get; set; }
    public required string ImageKey { get; set; }
    public required MemoryStream FileData { get; set; }
}
