using System.ComponentModel.DataAnnotations;
using SwagLabs.Models.Entities.Base;

namespace SwagLabs.Models.Entities;

public class Product : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }

}
