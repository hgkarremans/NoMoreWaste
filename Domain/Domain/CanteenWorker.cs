using System.ComponentModel.DataAnnotations;

namespace NoMoreWaste.Domain.DomainModels;

public class CanteenWorker
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;   
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public int PersonalNumber { get; set; }
    [Required]
    public Canteen Canteen { get; set; }= null!;
    public int CanteenId { get; set; } 
}