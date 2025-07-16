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

    [BindProperty]
    TEntity Entity { get; set; }

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
    [ActionName("Create")]
    public virtual async Task<IActionResult> CreatePostAsync()
    {
        if (ModelState.IsValid)
        {
            await MainDataService.AddAsync(Entity);
            return RedirectToAction(nameof(DetailsAsync).RemoveAsyncSuffix(), new { id = Entity.Id });
        }

        return View(Entity);
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
    [ActionName("Edit")]
    public virtual async Task<IActionResult> EditAsync(int id)
    {
        if (id != Entity.Id)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }

        if (ModelState.IsValid)
        {
            await MainDataService.UpdateAsync(Entity);
            return RedirectToAction(nameof(DetailsAsync).RemoveAsyncSuffix(), new { id });
        }

        return View(Entity);
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
    public virtual async Task<IActionResult> DeleteAsync(int id)
    {
        if (id != Entity.Id)
        {
            ViewData["Error"] = "Bad Request";
            return View();
        }

        try
        { 
            await MainDataService.DeleteAsync(Entity);
            return RedirectToAction(nameof(IndexAsync).RemoveAsyncSuffix());
        }
        catch (Exception ex)
        {
            ModelState.Clear();
            ModelState.AddModelError(string.Empty, ex.Message);
            Entity = await GetOneEntityAsync(Entity.Id);
            return View(Entity);
        }
    }
}
