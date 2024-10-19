using System.Collections.ObjectModel;
using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace Tests;

public class MealboxRepositoryTest : IAsyncLifetime
{
    private ApplicationDbContext _mockContext;
    private MealBoxRepository _repository;


    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


        _mockContext = new ApplicationDbContext(options);
        _repository = new MealBoxRepository(_mockContext);
    }

    public async Task DisposeAsync()
    {
        await _mockContext.DisposeAsync();
    }
    
    public async Task GetAllAsync_ShouldReturnAllMealboxes_WhenMealboxesExist()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var product = new Product
        {
            Id = 1, Name = "Apple", HasAlcohol = false, ImageUrl = "apple.jpg"
        };
        
        var mealbox1 = new MealBox() {
        Id = 1, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
        Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today, PickUpDate = DateTime.Today, ReservedStudent = 
        }
            
        _mockContext.MealBoxes.Add(mealbox1);
        _mockContext.MealBoxes.Add(mealbox2);
        _mockContext.SaveChanges();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
    
}