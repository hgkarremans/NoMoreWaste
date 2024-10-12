using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace UI.Models
{
    public class MealBoxViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Name for Mealbox")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Pick Up Date is required.")]
        [Display(Name = "Pick Up Date")]
        public DateTime PickUpDate { get; set; }

        [Required(ErrorMessage = "Expire Date is required.")]
        [Display(Name = "Expiration Date")]
        public DateTime ExpireDate { get; set; }

        [Required(ErrorMessage = "You must confirm if you are 18 or older.")]
        [Display(Name = "18+ Only")]
        public bool EighteenPlus { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Display(Name = "Price (€)")] 
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Meal Type is required.")]
        [Display(Name = "Type of Meal")]
        public MealType MealType { get; set; }

        public ICollection<int> SelectedProducts { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> Products { get; set; } = new List<SelectListItem>();
    }
}