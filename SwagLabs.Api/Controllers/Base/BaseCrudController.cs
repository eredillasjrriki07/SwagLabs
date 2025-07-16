using Microsoft.AspNetCore.Mvc;
using SwagLabs.Dal.Repos.Base;
using SwagLabs.Models.Entities.Base;
using SwagLabs.Services.Logging.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SwagLabs.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseCrudController<TEntity, TController> : ControllerBase
    where TEntity : BaseEntity, new()
    where TController : class
{

    protected readonly IBaseRepo<TEntity> MainRepo;
    
    protected readonly IAppLogging<TController> Logger;

    public BaseCrudController(IAppLogging<TController> logger, IBaseRepo<TEntity> repo)
    {
        MainRepo = repo;
        Logger = logger;
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was succesful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ApiVersion("1.0")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        var entities = await MainRepo.GetAllIgnoreQueryFiltersAsync();
        return Ok(entities);
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
    [HttpGet("{id}")]
    public async Task<ActionResult<TEntity>> GetOneAsync(int id)
    {
        var entity = await MainRepo.FindAsync(id);
        
        if (entity == null)
        {
            return NoContent();
        }

        return Ok(entity);
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was succesful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ApiVersion("1.0")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOneAsync(int id, TEntity entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try 
        {
            await MainRepo.UpdateAsync(entity);
        } 
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

        return Ok(entity);
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was succesful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ApiVersion("1.0")]
    [HttpPost]
    public async Task<ActionResult<TEntity>> AddOneAsync(TEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try 
        {
            await MainRepo.AddAsync(entity);
        } 
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

        return CreatedAtAction(nameof(GetOneAsync), new { id = entity.Id}, entity);
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was succesful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ApiVersion("1.0")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<TEntity>> DeleteOneAsync(int id, TEntity entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }

        try 
        {
            await MainRepo.DeleteAsync(entity);
        } 
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex.GetBaseException()?.Message);
        }

        return Ok();
    }
}
