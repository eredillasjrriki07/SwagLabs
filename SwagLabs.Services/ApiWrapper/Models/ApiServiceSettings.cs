
namespace SwagLabs.Services.ApiWrapper.Models;

public class ApiServiceSettings
{
    public ApiServiceSettings()
    {
    }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string Uri { get; set; }
    public string UserBaseUri { get; set; }
    public int MajorVersion { get; set; }
    public int MinorVersion { get; set; }
    public string Status { get; set; }
    public string ApiVersion => $"{MajorVersion}.{MinorVersion}" + (!string.IsNullOrEmpty(Status)? $"{Status}" : string.Empty);
}
