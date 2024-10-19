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
        var mealbox = await _dbContext.MealBoxes
            .Include(m => m.Products) 
            .FirstOrDefaultAsync(x => x.Id == id);

        if (mealbox == null)
        {
            throw new Exception($"MealBox with ID {id} not found.");
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
        var existingMealBox = await _dbContext.MealBoxes
            .Include(m => m.ReservedStudent)
            .FirstOrDefaultAsync(m => m.Id == mealbox.Id);

        if (existingMealBox == null)
        {
            throw new Exception("MealBox not found");
        }

        if (existingMealBox.ReservedStudent != null)
        {
            throw new Exception("Cannot update a reserved MealBox");
        }

        _dbContext.Entry(existingMealBox).CurrentValues.SetValues(mealbox);
        await _dbContext.SaveChangesAsync();
        return existingMealBox;
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

        if (mealBox.EighteenPlus && (mealBox.PickUpDate.Year - user.BirthDate.Year) < 18)
        {
            throw new Exception("MealBox is 18+");
        }

        var reservedMealboxes = await this.GetMyMealboxes(userId);
        foreach (var reservedMealbox in reservedMealboxes)
        {
            if (reservedMealbox.PickUpDate == mealBox.PickUpDate)
            {
                throw new Exception("You already have a mealbox reserved for this pickup date.");
            }
        }
        
        

        mealBox.ReservedStudent = user;
        await _dbContext.SaveChangesAsync();
        return mealBox;
    }

    public async Task<bool> IsMealBoxReserved(int mealBoxId)
    {
        var mealBox = await _dbContext.MealBoxes.Include(box => box.ReservedStudent)
            .FirstOrDefaultAsync(box => box.Id == mealBoxId);
        return mealBox.ReservedStudent != null;
    }
}