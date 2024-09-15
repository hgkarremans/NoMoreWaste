using NoMoreWaste.Domain.DomainModels.Enums;

namespace NoMoreWaste.Domain.DomainModels;

public class Canteen
{
    public int Id { get; set; }
    public City City { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public Boolean IsWarmFood { get; set; }
}