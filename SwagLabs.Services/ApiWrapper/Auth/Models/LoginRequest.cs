using System.ComponentModel.DataAnnotations;

namespace SwagLabs.Services.ApiWrapper.Auth.Models;

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
