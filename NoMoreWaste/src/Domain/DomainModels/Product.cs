namespace NoMoreWaste.Domain.DomainModels;

public class Product
{
    public int Id { get; set; }
    public Boolean HasAlchol { get; set; }
    public string ImageUrl { get; set; } = null!;
    
}