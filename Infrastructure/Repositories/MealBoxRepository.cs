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
        // Use Include to load related Products if necessary
        var mealbox = await _dbContext.MealBoxes
            .Include(m => m.Products) // Ensure related Products are included
            .FirstOrDefaultAsync(x => x.Id == id);
    
        // Check if mealbox is null and handle accordingly
        if (mealbox == null)
        {
            // Option 1: Throw a custom exception (you may want to define your own exception class)
            throw new Exception($"MealBox with ID {id} not found.");

            // Option 2: Return null if you prefer not to use exceptions for not found
            // return null;
        }

        return mealbox;
    }


    public async Task<List<MealBox>> GetAllAsync()
    {
        return await _dbContext.MealBoxes.ToListAsync();
    }

    public async Task<List<MealBox>> GetMyMealboxes(int userId)
    {
        return await _dbContext.MealBoxes.Include(box => box.ReservedStudent)
            .Where(box => box.ReservedStudent.Id == userId).ToListAsync();
    }

    public async Task<List<MealBox>> GetAllAvailableAsync()
    {
        return await _dbContext.MealBoxes.Include(box => box.ReservedStudent)
            .Where(box => box.ReservedStudent == null).ToListAsync();
    }
    public async Task<List<MealBox>> GetCanteenMealboxesAsync(int canteenId)
    {
        return await _dbContext.MealBoxes.Include(box => box.ReservedStudent)
            .Where(box => box.Canteen.Id == canteenId).ToListAsync();
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
        if (mealbox.ReservedStudent != null)
        {
            throw new Exception("MealBox is reserved");
        }
        _dbContext.MealBoxes.Remove(mealbox);
        await _dbContext.SaveChangesAsync();
        return mealbox;
    }
    public async Task<MealBox> ReservateMealBoxAsync(int mealBoxId, int userId)
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
        await _dbContext.SaveChangesAsync();
        return mealBox;
    }
    
}