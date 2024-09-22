namespace NoMoreWaste.Domain.DomainModels;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Boolean HasAlcohol { get; set; }
    public string ImageUrl { get; set; } = null!;
    
}