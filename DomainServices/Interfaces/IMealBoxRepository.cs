using NoMoreWaste.Domain.DomainModels;

namespace Application;

public interface IMealBoxRepository
{
    Task<MealBox> GetByIdAsync(int id);
    Task<List<MealBox>> GetAllAsync();
    Task<List<MealBox>> GetMyMealboxes(int userId);
    Task<MealBox> CreateAsync(MealBox mealbox);
    Task<MealBox> UpdateAsync(MealBox mealbox);
    Task<MealBox> DeleteAsync(MealBox mealbox);
    Task<MealBox> ReservateMealBoxAsync(int mealBoxId, int userId);
    Task<List<MealBox>> GetAllAvailableAsync();
    Task<List<MealBox>> GetCanteenMealboxesAsync(int canteenId);
    Task<bool> IsMealBoxReserved(int mealBoxId);
    
    
}