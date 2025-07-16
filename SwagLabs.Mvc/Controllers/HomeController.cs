using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SwagLabs.Models.Entities;
using SwagLabs.Mvc.Models;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Mvc.Controllers;

public class HomeController : Controller 
{
    private readonly IAppLogging<HomeController> _logger;

    public HomeController(IAppLogging<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> RazorSyntax([FromServices] IUserDataService dataService)
    {
        User user = await dataService.FindAsync(1);
        return View(user);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
