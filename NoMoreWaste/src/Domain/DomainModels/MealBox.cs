using NoMoreWaste.Domain.DomainModels.Enums;

namespace NoMoreWaste.Domain.DomainModels;

public class MealBox
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public City City { get; set; }
    public Canteen Canteen { get; set; } = null!;
    public DateTime PickUpDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public bool EighteenPlus { get; set; }
    public decimal Price { get; set; }
    public MealType MealType { get; set; }
    public Student? ReservedStudent { get; set; }

    public ICollection<Product> Products { get; set; } = null!;
    
    
    
}