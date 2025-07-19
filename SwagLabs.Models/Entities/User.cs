
using System.ComponentModel.DataAnnotations;
using SwagLabs.Models.Entities.Base;

namespace SwagLabs.Models.Entities;

public class User : BaseEntity
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}
