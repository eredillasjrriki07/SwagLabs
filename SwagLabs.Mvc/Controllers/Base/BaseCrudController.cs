using Microsoft.AspNetCore.Mvc;
using SwagLabs.Models.Entities.Base;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;
using SwagLabs.Services.Utilities;

namespace SwagLabs.Mvc.Controllers.Base;

[Route("[controller]/[action]")]
public abstract class BaseCrudController<TEntity, TController> : Controller
    where TEntity : BaseEntity, new()
    where TController : class
{

    protected readonly IAppLogging<TController> AppLoggingInstance;

    protected IDataServiceBase<TEntity> MainDataService;

    protected BaseCrudController(IAppLogging<TController> appLogging, IDataServiceBase<TEntity> mainDataService)
    {
        AppLoggingInstance = appLogging;
        MainDataService = mainDataService;
    }

    [HttpGet]
    [Route("/[controller]")]
    [Route("/[controller]/[action]")]
    public virtual async Task<IActionResult> IndexAsync()
    {
        return View(await MainDataService.GetAllAsync());
    }

    protected async Task<TEntity> GetOneEntityAsync(int id)
        => await MainDataService.FindAsync(id);

    [HttpGet("{id?}")]
    public virtual async Task<IActionResult> DetailsAsync(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }

        TEntity entity = await GetOneEntityAsync(id.Value);

        if (entity == null)
        {
            return NotFound();
        }

        return View(entity);
    }

    [HttpGet]
    public virtual async Task<IActionResult> CreateAsync()
    {
        return View();
    }

    [HttpPost]
    public virtual async Task<IActionResult> CreateAsync(TEntity entity)
    {
        if (ModelState.IsValid)
        {
            await MainDataService.AddAsync(entity);
            return RedirectToAction(nameof(DetailsAsync).RemoveAsyncSuffix(), new { id = entity.Id });
        }

        return View(entity);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> EditAsync(int? id)
    {
        if (!id.HasValue)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }

        TEntity entity = await GetOneEntityAsync(id.Value);

        if (entity == null)
        {
            ViewData["Error"] = "Not Found";
            return View();
        }

        return View(entity);
    }

    [HttpPost("{id}")]
    public virtual async Task<IActionResult> EditAsync(int id, TEntity entity)
    {
        if (id != entity.Id)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }

        if (ModelState.IsValid)
        {
            await MainDataService.UpdateAsync(entity);
            return RedirectToAction(nameof(DetailsAsync).RemoveAsyncSuffix(), new { id });
        }

        return View(entity);
    }

    [HttpGet("{id?}")]
    public virtual async Task<IActionResult> DeleteAsync(int? id)
    {
        if (!id.HasValue)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }

        TEntity entity = await GetOneEntityAsync(id.Value);

        if (entity == null)
        {
            ViewData["Error"] = "Not Found";
            return View();
        }

        return View(entity);
    }

    [HttpPost("{id}")]
    [ActionName("Delete")]
    public virtual async Task<IActionResult> DeleteAsync(int id, TEntity entity)
    {
        if (id != entity.Id)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }

        try
        { 
            await MainDataService.DeleteAsync(entity);
            return RedirectToAction(nameof(IndexAsync).RemoveAsyncSuffix());
        }
        catch (Exception ex)
        {
            ModelState.Clear();
            ModelState.AddModelError(string.Empty, ex.Message);
            entity = await GetOneEntityAsync(entity.Id);
            return View(entity);
        }
    }
}
