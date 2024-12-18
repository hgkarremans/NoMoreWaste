using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;

namespace Application.Repositories;

public class CanteenWorkerRepository : ICanteenWorkerRepository
{
    private readonly ApplicationDbContext _context;

    public CanteenWorkerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CanteenWorker> GetByIdAsync(int id)
    {
        var canteenWorker = await _context.CanteenWorkers.FirstOrDefaultAsync(x => x.Id == id);
        return canteenWorker ?? throw new KeyNotFoundException($"CanteenWorker with id {id} not found.");
    }


    public async Task<List<CanteenWorker>> GetAllAsync()
    {
        return await _context.CanteenWorkers.ToListAsync();
    }

    public async Task<CanteenWorker> CreateAsync(CanteenWorker canteenWorker)
    {
        _context.CanteenWorkers.Add(canteenWorker);
        await _context.SaveChangesAsync();
        return canteenWorker;
    }

    public async Task<CanteenWorker> UpdateAsync(CanteenWorker canteenWorker)
    {
        var canteenWorkerToUpdate = await _context.CanteenWorkers.FirstOrDefaultAsync(x => x.Id == canteenWorker.Id);
        if (canteenWorkerToUpdate == null)
        {
            throw new KeyNotFoundException($"CanteenWorker not found");
        }
        _context.CanteenWorkers.Update(canteenWorker);
        await _context.SaveChangesAsync();
        return canteenWorker;
    }

    public async Task<CanteenWorker> DeleteAsync(CanteenWorker canteenWorker)
    {
        var canteenWorkerToDelete = await _context.CanteenWorkers.FirstOrDefaultAsync(x => x.Id == canteenWorker.Id);
        if (canteenWorkerToDelete == null)
        {
            throw new KeyNotFoundException("CanteenWorker not found");
        }

        _context.CanteenWorkers.Remove(canteenWorker);
        await _context.SaveChangesAsync();
        return canteenWorker;
    }

    public async Task<int> GetCanteenByUserEmail(string email)
    {
        var canteenWorker = await _context.CanteenWorkers.FirstOrDefaultAsync(x => x.Email == email);
        if (canteenWorker == null)
        {
            throw new KeyNotFoundException($"CanteenWorker with email {email} not found.");
        }

        return canteenWorker.CanteenId;
    }
}