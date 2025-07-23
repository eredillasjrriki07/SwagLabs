using Microsoft.AspNetCore.Mvc;
using SwagLabs.Api.Controllers.Auth.Models;
using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;
using SwagLabs.Services.Logging.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SwagLabs.Api.Controllers.Auth;

[ApiController]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    protected readonly IUserRepo UserRepo;
    protected readonly IAppLogging<AuthController> AppLoggingInstance;

    public AuthController(IAppLogging<AuthController> appLogging, IUserRepo userRepo)
    {
        AppLoggingInstance = appLogging;
        UserRepo = userRepo;
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was succesful")]
    [SwaggerResponse(204, "No content")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ApiVersion("1.0")]
    [HttpPost]
    public async Task<ActionResult<User>> LoginAsync(LoginRequest loginRequest)
    {
        var entity = await UserRepo.FindByUsernameAsync(loginRequest.UserName);

        if (entity == null)
        {
            return NoContent();
        }

        return Ok(entity);
    }
}
