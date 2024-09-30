using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
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
        var mealbox = await _dbContext.MealBoxes.FirstOrDefaultAsync(x => x.Id == id);
        if (mealbox == null)
        {
            throw new Exception("MealBox not found");
        }
        return mealbox;
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
    public MealBox ReservateMealBoxAsync(int mealBoxId, int userId)
    {
        var mealBox = _dbContext.MealBoxes.Include(
                box => box.Products).Include(box => box.ReservedStudent)
            .FirstOrDefault(box => box.Id == mealBoxId);
        var user = _dbContext.Students.FirstOrDefault(student => student.Id == userId);
        if (mealBox == null)
        {
            throw new Exception("MealBox not found");
        }
        if (mealBox.ReservedStudent != null)
        {
            throw new Exception("MealBox already reserved");
        }

        mealBox.ReservedStudent = user;
        _dbContext.SaveChanges();
        return mealBox;
    }
    
}