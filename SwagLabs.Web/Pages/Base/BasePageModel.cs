using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SwagLabs.Models.Entities.Base;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Web.Pages.Base;

public abstract class BasePageModel<TEntity, TPageModel> : PageModel
    where TEntity : BaseEntity, new()
{
    protected readonly IAppLogging<TPageModel> AppLoggingInstance;

    protected readonly IDataServiceBase<TEntity> DataService;

    [ViewData]
    public string Title { get; init; }

    [BindProperty]
    public TEntity Entity { get; set; }

    public string Error { get; set; }


    public BasePageModel(IAppLogging<TPageModel> appLogging, IDataServiceBase<TEntity> dataService, string title)
    {
        AppLoggingInstance = appLogging;
        DataService = dataService;
        Title = title;
    }

    protected async Task GetOneAsync(int? id)
    {
        if (!id.HasValue)
        {
            Error = "Invalid request";
            Entity = null;
            return;
        }

        Entity = await DataService.FindAsync(id.Value);

        if (Entity == null)
        {
            Error = "Not found";
            return;
        }

        Error = string.Empty;
    }

    protected virtual async Task<IActionResult> SaveOneAsync(Func<TEntity, bool, Task<TEntity>> persistenceTask)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            await persistenceTask(Entity, true);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
            ModelState.AddModelError(string.Empty, ex.Message);
            AppLoggingInstance.LogAppError(ex, "An error occured");
            return Page();
        }

        return RedirectToPage("./Details", new { id = Entity.Id });

    }

    public async Task<IActionResult> DeleteOneAsync(int? id)
    {
        if (!id.HasValue || id.Value != Entity.Id)
        {
            Error = "Bad Request";
            return Page();
        }

        try
        {
            await DataService.DeleteAsync(Entity);
            return RedirectToPage("./Index");
        }
        catch (Exception ex)
        {
            ModelState.Clear();
            Entity = await DataService.FindAsync(id.Value);
            Error = ex.Message;
            AppLoggingInstance.LogAppError(ex, "An error occured");
            return Page();
        }
    }
}
