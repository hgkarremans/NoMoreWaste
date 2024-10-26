using System.ComponentModel.DataAnnotations;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace NoMoreWaste.Domain.DomainModels;

public class Student
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public int StudentNumber { get; set; }
    [Required]
    public City City { get; set; }
    [Required]
    public int NoShows { get; set; }
}