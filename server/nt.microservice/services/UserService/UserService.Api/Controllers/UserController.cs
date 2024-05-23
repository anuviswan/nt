using Azure;
using MassTransit;
using nt.shared.dto.User;
using UserService.Api.ViewModels.User;
using UserService.Service.Query;

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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("uploadprofileimage")]
   // [Authorize]
    public async Task<IActionResult> UpdateProfileImage([FromForm]UpdateProfileImageRequestViewModel updateProfileImage)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var memoryStream = new MemoryStream())
            {
                updateProfileImage.File.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                var uploadImageCommand = Mapper.Map<UploadProfileImageCommand>(updateProfileImage);
                uploadImageCommand.FileData = memoryStream;
                uploadImageCommand.ImageKey = updateProfileImage.ImageKey;
                var result = await Mediator.Send(uploadImageCommand).ConfigureAwait(false);
            }
            
            return Ok("Image uploaded");
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Request failed: {ex.Message}");
            Console.WriteLine($"Status: {ex.Status}");
            Console.WriteLine($"ErrorCode: {ex.ErrorCode}");
            Console.WriteLine($"Additional Information: {ex.Data}");
            return BadRequest(ex);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("getprofileimage")]
    public async Task<IActionResult> GetProfileImage(string userName)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(new GetProfileImageQuery() { UserName = userName}).ConfigureAwait(false);
            return File(result,"image/jpeg");
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

}
