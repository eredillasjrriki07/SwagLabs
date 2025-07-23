using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Auth.Models;
using SwagLabs.Services.AuthServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;
using SwagLabs.Web.ViewModel;

namespace SwagLabs.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {

        protected readonly IAppLogging<LoginModel> AppLoggingInstance;

        protected readonly IAuthService AuthService;

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public string Error { get; set; }

        public LoginModel(IAppLogging<LoginModel> appLogging, IAuthService authService)
        {
            AppLoggingInstance = appLogging;
            AuthService = authService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            LoginRequest loginRequest = new()
            {
                UserName = LoginViewModel.UserName,
                Password = LoginViewModel.Password,
            };

            User user = await AuthService.LoginAsync(loginRequest);

            if (user == null)
            {
                Error = "Invalid credentials";
                return Page();
            }

            Console.WriteLine(user.UserName);
            Console.WriteLine(user.Password);

            return RedirectToPage("/Users/Index");
        }
    }
}
