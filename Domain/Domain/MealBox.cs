using System.ComponentModel.DataAnnotations;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace NoMoreWaste.Domain.DomainModels;

public class MealBox
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public City City { get; set; }
    public Canteen? Canteen { get; set; } = null!;
    [Required(ErrorMessage = "Canteen is required")]
    public int CanteenId { get; set; } 
    [Required]
    public DateTime PickUpDate { get; set; }
    [Required]
    public DateTime ExpireDate { get; set; }
    [Required]
    public bool EighteenPlus { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public MealType MealType { get; set; }
    public Student? ReservedStudent { get; set; }
    [Required]

    public ICollection<Product> Products { get; set; } = null!;
    
    
    
    
}