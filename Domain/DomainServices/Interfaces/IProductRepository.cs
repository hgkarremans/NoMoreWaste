using NoMoreWaste.Domain.DomainModels;

namespace Application;

public interface IProductRepository
{
    
    Task<Product> GetByIdAsync(string id);
    Task<List<Product>> GetAllAsync();
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<Product> DeleteAsync(Product product);
    
}