using System.ComponentModel;
using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;

namespace Tests;

public class ProductRepositoryTest : IAsyncLifetime
{
    
    private ApplicationDbContext _mockContext;
    private ProductRepository _repository;


    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


        _mockContext = new ApplicationDbContext(options);
        _repository = new ProductRepository(_mockContext);
    }

    public async Task DisposeAsync()
    {
        await _mockContext.DisposeAsync();
    }
    
    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        // Arrange
        var product = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };

        _mockContext.Products.Add(product);
        _mockContext.SaveChanges();

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Apple", result.Name);
    }
    
    [Fact] 
    public async Task GetByIdAsync_ShouldNotReturnProducts_WhenProductsDoNotExist()
    {
        // Arrange
        var product = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };

        _mockContext.Products.Add(product);
        _mockContext.SaveChanges();

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(() => _repository.GetByIdAsync(2));

        // Assert
        Assert.Equal("Product not found", exception.Message);
    }
    [Fact] 
    public async Task GetAllAsync_ShouldReturnAllProducts()
    {
        // Arrange
        var product1 = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };
        var product2 = new Product
        {
            Id = 2, Name = "Banana", HasAlcohol = false, ImageUrl = "banana.jpg"
        };
        var product3 = new Product
        {
            Id = 3, Name = "Beer", HasAlcohol = true, ImageUrl = "beer.jpg"
        };

        _mockContext.Products.Add(product1);
        _mockContext.Products.Add(product2);
        _mockContext.Products.Add(product3);
        _mockContext.SaveChanges();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }
    
    [Fact] 
    public async Task CreateAsync_ShouldCreateProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };

        // Act
        var result = await _repository.CreateAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Apple", result.Name);
    }
    [Fact] 
    public async Task UpdateAsync_ShouldUpdateProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };

        _mockContext.Products.Add(product);
        _mockContext.SaveChanges();

        // Act
        product.Name = "Banana";
        var result = await _repository.UpdateAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Banana", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldNotUpdateProduct()
    {
        var product = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };
        
        //act 
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.UpdateAsync(product));
        
        //assert
        Assert.Equal("Product not found", exception.Result.Message);
    }
    
    [Fact] 
    public async Task DeleteAsync_ShouldDeleteProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };

        _mockContext.Products.Add(product);
        _mockContext.SaveChanges();

        // Act
        var result = await _repository.DeleteAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Apple", result.Name);
    }
    
    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteProduct()
    {
        var product = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };
        
        //act 
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.DeleteAsync(product));
        
        //assert
        Assert.Equal("Product not found", exception.Result.Message);
    }
    
}