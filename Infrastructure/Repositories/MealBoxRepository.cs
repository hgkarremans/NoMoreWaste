using NoMoreWaste.Domain.DomainModels;

namespace Application.Repositories;

public class MealBoxRepository : IMealBoxRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MealBoxRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MealBox> GetByIdAsync(int id)
    {
        return await _dbContext.MealBoxes.FindAsync(id);
    }

    public async Task<List<MealBox>> GetAllAsync()
    {
        return await _dbContext.MealBoxes.ToListAsync();
    }

    public async Task<MealBox> CreateAsync(MealBox mealbox)
    {
        await _dbContext.MealBoxes.AddAsync(mealbox);
        await _dbContext.SaveChangesAsync();
        return mealbox;
    }

    public async Task<MealBox> UpdateAsync(MealBox mealbox)
    {
        _dbContext.MealBoxes.Update(mealbox);
        await _dbContext.SaveChangesAsync();
        return mealbox;
    }

    public async Task<MealBox> DeleteAsync(MealBox mealbox)
    {
        _dbContext.MealBoxes.Remove(mealbox);
        await _dbContext.SaveChangesAsync();
        return mealbox;
    }
    
}