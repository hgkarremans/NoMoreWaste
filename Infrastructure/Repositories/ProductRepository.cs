using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;

namespace Application.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        return product;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var productToUpdate = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
        if (productToUpdate == null)
        {
            throw new Exception("Product not found");
        }

        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> DeleteAsync(Product product)
    {
        var productToDelete = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
        if (productToDelete == null)
        {
            throw new Exception("Product not found");
        }

        _context.Products.Remove(productToDelete);
        await _context.SaveChangesAsync();
        return productToDelete;
    }
    
}