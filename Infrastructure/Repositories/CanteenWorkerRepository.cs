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
        return await _context.CanteenWorkers.FindAsync(id);
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
        _context.CanteenWorkers.Update(canteenWorker);
        await _context.SaveChangesAsync();
        return canteenWorker;
    }

    public async Task<CanteenWorker> DeleteAsync(CanteenWorker canteenWorker)
    {
        _context.CanteenWorkers.Remove(canteenWorker);
        await _context.SaveChangesAsync();
        return canteenWorker;
    }
    
}