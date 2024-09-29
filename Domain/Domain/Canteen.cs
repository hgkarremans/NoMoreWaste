using System.ComponentModel.DataAnnotations;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace NoMoreWaste.Domain.DomainModels;

public class Canteen
{
    [Key]
    public string Id { get; set; }
    [Required]
    public City City { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    public Boolean IsWarmFood { get; set; }
}