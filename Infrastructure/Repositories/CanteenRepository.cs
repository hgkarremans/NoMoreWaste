using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;

namespace Application.Repositories;

public class CanteenRepository : ICanteenRepository
{
    private readonly ApplicationDbContext _context;

    public CanteenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Canteen> GetByIdAsync(string id)
{
    var canteen = await _context.Canteens.FirstOrDefaultAsync(x => x.Id == id);
    return canteen ?? throw new KeyNotFoundException($"Canteen with id {id} not found.");
}

    public async Task<List<Canteen>> GetAllAsync()
    {
        return await _context.Canteens.ToListAsync();
    }

    public async Task<Canteen> CreateAsync(Canteen canteen)
    {
        _context.Canteens.Add(canteen);
        await _context.SaveChangesAsync();
        return canteen;
    }

    public async Task<Canteen> UpdateAsync(Canteen canteen)
    {
        _context.Canteens.Update(canteen);
        await _context.SaveChangesAsync();
        return canteen;
    }

    public async Task<Canteen> DeleteAsync(Canteen canteen)
    {
        _context.Canteens.Remove(canteen);
        await _context.SaveChangesAsync();
        return canteen;
    }
    
   }