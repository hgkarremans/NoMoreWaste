using Application;
using NoMoreWaste.Domain.DomainModels;

namespace VoedselApi.GraphQL;

public class Query
{
    private readonly IMealBoxRepository _mealBoxRepository;

    public Query(IMealBoxRepository mealBoxRepository)
    {
        _mealBoxRepository = mealBoxRepository;
    }
    public async Task<IEnumerable<MealBox>> GetMealBoxes() => await _mealBoxRepository.GetAllAsync();
    
    public async Task<MealBox> GetMealBox(int id) => await _mealBoxRepository.GetByIdAsync(id);
}