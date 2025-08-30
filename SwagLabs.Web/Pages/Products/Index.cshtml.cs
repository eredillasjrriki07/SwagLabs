using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SwagLabs.Models.Entities;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IAppLogging<IndexModel> _appLogging;

    private readonly IProductDataService _productService;

    [ViewData]
    public string Title => "Products";

    [ViewData]
    public string Error { get; set; }

    public IEnumerable<Product> Products { get; set; }

    public IndexModel(IAppLogging<IndexModel> appLogging, IProductDataService productService)
    {
        _appLogging = appLogging;
        _productService = productService;
    }

    public async Task OnGet()
    {
        try
        {
            Products = await _productService.GetAllAsync();
        }
        catch (UnauthorizedAccessException)
        {
            Error = "401 Unauthorized";
        }

    }
}
