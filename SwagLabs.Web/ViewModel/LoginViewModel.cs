using System.ComponentModel.DataAnnotations;

namespace SwagLabs.Web.ViewModel;

public class LoginViewModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

}
