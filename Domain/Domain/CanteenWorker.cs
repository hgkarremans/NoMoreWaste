using System.ComponentModel.DataAnnotations;

namespace NoMoreWaste.Domain.DomainModels;

public class CanteenWorker
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;   
    [Required]
    public int PersonalNumber { get; set; }
    [Required]
    public Canteen Canteen { get; set; } = null!; 
}