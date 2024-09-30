using NoMoreWaste.Domain.DomainModels;

namespace Application;

public interface ICanteenRepository
{
    Task<Canteen> GetByIdAsync(string id);
    Task<List<Canteen>> GetAllAsync();
    Task<Canteen> CreateAsync(Canteen canteen);
    Task<Canteen> UpdateAsync(Canteen canteen);
    Task<Canteen> DeleteAsync(Canteen canteen);
    
}