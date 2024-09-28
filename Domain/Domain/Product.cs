using System.ComponentModel.DataAnnotations;

namespace NoMoreWaste.Domain.DomainModels;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public Boolean HasAlcohol { get; set; }
    [Required]
    public string ImageUrl { get; set; } = null!;
    public ICollection<MealBox> MealBoxes { get; set; } = null!;
    
}