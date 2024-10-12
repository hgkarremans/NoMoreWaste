using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace UI.Models;

public class MealBoxViewModel
{
    [Required]
    public string Name { get; set; } = null!;
    
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

    public ICollection<int> SelectedProducts { get; set; } = new List<int>();

    public IEnumerable<SelectListItem> Products { get; set; } = new List<SelectListItem>();
}