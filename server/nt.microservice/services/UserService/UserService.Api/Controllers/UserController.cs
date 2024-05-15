using MassTransit;
using nt.shared.dto.User;
using UserService.Api.ViewModels.User;
using UserService.Domain.Entities;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/Users")]
public class UserController : BaseController
{
    private readonly IPublishEndpoint  _publishEndPoint;
    public UserController(IMediator mediator,IMapper mapper, ILogger<UserController> logger, IPublishEndpoint publishEndPoint):base(mediator,mapper,logger)
    {
        _publishEndPoint = publishEndPoint;
    }

    /// <summary>
    /// Creates a new User if not already present.
    /// </summary>
    /// <param name="user">User information to be created</param>
    /// <returns>User information if created successfully</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("Register")]
    
    public async Task<ActionResult<CreateUserResponseViewModel>> RegisterUser(CreateUserRequestViewModel user)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create User Meta information in User Service
            var result = await Mediator.Send(Mapper.Map<CreateUserCommand>(user)).ConfigureAwait(false);

            // Now create User Authentication information in AuthService
            await _publishEndPoint.Publish<CreateUserInitiated>(Mapper.Map<CreateUserInitiated>(user)).ConfigureAwait(false);

            return new OkObjectResult(Mapper.Map<CreateUserResponseViewModel>(result));
        }
        catch(Exception ex)
        {
            Logger.LogError($"Error registering user : {ex.Message}");
            return BadRequest(ex.Message);
        }
    }


    public async Task<IActionResult> UpdateProfileImage(UpdateProfileImageRequestViewModel updateProfileImage)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using var memoryStream = new MemoryStream();
            updateProfileImage.ImageData.File.CopyTo(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var uploadImageCommand = Mapper.Map<UploadImageCommand>(updateProfileImage);
            uploadImageCommand.FileData = memoryStream;
            uploadImageCommand.ImageKey = updateProfileImage.UserName;
            var result = await Mediator.Send(uploadImageCommand).ConfigureAwait(false);
            return Ok("Image uploaded");
        }
        catch (Exception e)
        {
            return BadRequest($"Error: {e}");
        }
    }

}
