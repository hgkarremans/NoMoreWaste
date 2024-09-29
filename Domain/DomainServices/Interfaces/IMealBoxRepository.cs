using NoMoreWaste.Domain.DomainModels;

namespace Application;

public interface IMealBoxRepository
{
    Task<MealBox> GetByIdAsync(string id);
    Task<List<MealBox>> GetAllAsync();
    Task<MealBox> CreateAsync(MealBox mealbox);
    Task<MealBox> UpdateAsync(MealBox mealbox);
    Task<MealBox> DeleteAsync(MealBox mealbox);
    
    
}