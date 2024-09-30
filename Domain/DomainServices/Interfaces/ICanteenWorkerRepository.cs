using NoMoreWaste.Domain.DomainModels;

namespace Application;

public interface ICanteenWorkerRepository
{
    
    Task<CanteenWorker> GetByIdAsync(string id);
    Task<List<CanteenWorker>> GetAllAsync();
    Task<CanteenWorker> CreateAsync(CanteenWorker canteenWorker);
    Task<CanteenWorker> UpdateAsync(CanteenWorker canteenWorker);
    Task<CanteenWorker> DeleteAsync(CanteenWorker canteenWorker);
    
}