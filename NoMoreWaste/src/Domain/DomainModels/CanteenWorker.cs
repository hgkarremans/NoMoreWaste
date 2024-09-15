namespace NoMoreWaste.Domain.DomainModels;

public class CanteenWorker
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int PersonalNumber { get; set; }
    public Canteen Canteen { get; set; } = null!; 
}