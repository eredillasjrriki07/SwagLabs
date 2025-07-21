using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SwagLabs.Models.Entities;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Web.Pages.Users;

public class IndexModel : PageModel
{
    private readonly IAppLogging<IndexModel> _appLogging;
    
    private readonly IUserDataService _userService;

    [ViewData]
    public string Title => "Users";

    public IEnumerable<User> UserRecords { get; set; }

    public IndexModel(IAppLogging<IndexModel> appLogging, IUserDataService userService)
    {
        _appLogging = appLogging;
        _userService = userService;
    }

    public async Task OnGetAsync()
    {
        UserRecords = await _userService.GetAllAsync();
    }
}
