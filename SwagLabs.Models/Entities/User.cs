
using SwagLabs.Models.Entities.Base;

namespace SwagLabs.Models.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
