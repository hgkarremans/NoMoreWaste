using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NoMoreWaste.Domain.DomainModels;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace Tests;

public class CanteenWorkerRepositoryTest : IAsyncLifetime
{
    private ApplicationDbContext _mockContext;
    private CanteenWorkerRepository _repository;


    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


        _mockContext = new ApplicationDbContext(options);
        _repository = new CanteenWorkerRepository(_mockContext);
    }

    public async Task DisposeAsync()
    {
        await _mockContext.DisposeAsync();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCanteenWorker_WhenCanteenWorkerExists()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };

        var canteenWorker = new CanteenWorker
        {
            Id = 1, Name = "John Doe", PersonalNumber = 213344,
            Email = "john.doe@gmail.com", CanteenId = 1, Canteen = canteen
        };
        _mockContext.CanteenWorkers.Add(canteenWorker);
        _mockContext.SaveChanges();

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenCanteenWorkerDoesNotExist()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };

        var canteenWorker = new CanteenWorker
        {
            Id = 1, Name = "John Doe", PersonalNumber = 213344,
            Email = "john.doe@gmail.com", CanteenId = 1, Canteen = canteen
        };
        _mockContext.CanteenWorkers.Add(canteenWorker);
        _mockContext.SaveChanges();

        // Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.GetByIdAsync(2));

        // Assert
        Assert.Contains("CanteenWorker with id 2 not found.", exception.Message);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfCanteenWorkers()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };

        var canteenWorker = new CanteenWorker
        {
            Id = 1, Name = "John Doe", PersonalNumber = 213344,
            Email = "john.doe@gmail.com", CanteenId = 1, Canteen = canteen
        };

        _mockContext.CanteenWorkers.Add(canteenWorker);
        _mockContext.SaveChanges();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("John Doe", result.First().Name);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedCanteenWorker()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };

        var canteenWorker = new CanteenWorker
        {
            Id = 1, Name = "John Doe", PersonalNumber = 213344,
            Email = "john.doe@gmail.com", CanteenId = 1, Canteen = canteen
        };


        //Act

        var result = await _repository.CreateAsync(canteenWorker);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUpdatedCanteenWorker()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };

        var canteenWorker = new CanteenWorker
        {
            Id = 1, Name = "John Doe", PersonalNumber = 213344,
            Email = "john.doe@gmail.com", CanteenId = 1, Canteen = canteen
        };

        var updatedCanteenWorker = new CanteenWorker()
        {
            Id = 1, Name = "Jane Doe", PersonalNumber = 213344,
            Email = "test@gmail.com", CanteenId = 1, Canteen = canteen
        };

        _mockContext.CanteenWorkers.Add(canteenWorker);
        _mockContext.SaveChanges();

        //Act
        var result = _repository.UpdateAsync(updatedCanteenWorker);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnDeletedCanteenWorker()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };

        var canteenWorker = new CanteenWorker
        {
            Id = 1, Name = "John Doe", PersonalNumber = 213344,
            Email = "john.doe@gmail.com", CanteenId = 1, Canteen = canteen
        };
        _mockContext.CanteenWorkers.Add(canteenWorker);
        _mockContext.SaveChanges();

        //Act
        var result = await _repository.DeleteAsync(canteenWorker);


        //Assert 
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetCanteenByUserEmail_ShouldReturnCanteenId()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };

        var canteenWorker = new CanteenWorker
        {
            Id = 1, Name = "John Doe", PersonalNumber = 213344,
            Email = "john.doe@gmail.com", CanteenId = 1, Canteen = canteen
        };
        _mockContext.CanteenWorkers.Add(canteenWorker);
        _mockContext.SaveChanges();

        //Act 
        var result = _repository.GetCanteenByUserEmail("john.doe@gmail.com");

        //Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetCanteenByUserEmail_ShouldReturnZero()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };

        var canteenWorker = new CanteenWorker
        {
            Id = 1, Name = "John Doe", PersonalNumber = 213344,
            Email = "john.doe@gmail.com", CanteenId = 1, Canteen = canteen
        };
        _mockContext.CanteenWorkers.Add(canteenWorker);
        _mockContext.SaveChanges();
        
        //Act
        var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.GetCanteenByUserEmail("ava.lovelace@gmail.com"));
        

        //Assert
        Assert.Equal(0, result);
        Assert.Equal("CanteenWorker with email ava.lovelace@gmail.com not found.", result);
    }
}

    
    
    

        