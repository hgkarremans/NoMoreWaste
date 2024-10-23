using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;
using NoMoreWaste.Domain.DomainModels.Enums;
using Xunit;

namespace Tests
{
    public class CanteenRepositoryTests : IAsyncLifetime
    {
        private ApplicationDbContext _mockContext;
        private CanteenRepository _repository;


        public async Task InitializeAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
        
            _mockContext = new ApplicationDbContext(options);
            _repository = new CanteenRepository(_mockContext);
        }
        
        public async Task DisposeAsync()
        {
            await _mockContext.DisposeAsync();
        }

        [Fact]  
        public async Task GetByIdAsync_ShouldReturnCanteen_WhenCanteenExists()
        {
            // Arrange
            var canteen = new Canteen { Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};
            _mockContext.Canteens.Add(canteen);
            _mockContext.SaveChanges();

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Main Canteen", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenCanteenDoesNotExist()
        {
            // Arrange
            var canteen = new Canteen { Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};
            _mockContext.Canteens.Add(canteen);
            _mockContext.SaveChanges();

            // Act
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.GetByIdAsync(2));

            // Assert
            Assert.Contains("Canteen with id 2 not found.", exception.Message);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfCanteens()
        {
            // Arrange
            var canteen1 = new Canteen { Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};
            var canteen2 = new Canteen { Id = 2, Name = "Second Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};
            _mockContext.Canteens.Add(canteen1);
            _mockContext.Canteens.Add(canteen2);
            _mockContext.SaveChanges();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Main Canteen", result.First().Name);
            Assert.Equal("Second Canteen", result.Last().Name);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedCanteen()
        {
            // Arrange
            var canteen = new Canteen { Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};

            // Act
            var result = await _repository.CreateAsync(canteen);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Main Canteen", result.Name);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnUpdatedCanteen()
        {
            // Arrange
            var canteen = new Canteen { Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};
            _mockContext.Canteens.Add(canteen);
            _mockContext.SaveChanges();
            canteen.Name = "Updated Canteen";

            // Act
            var result = await _repository.UpdateAsync(canteen);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Updated Canteen", result.Name);
        }
        [Fact]
        public async Task UpdateAsync_ShouldNotreturnUpdatedCanteen()
        {
            var canteen = new Canteen { Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};
            
            // act
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.UpdateAsync(canteen));
            
            // assert
            Assert.Equal("Canteen not found", exception.Result.Message);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnDeletedCanteen()
        {
            // Arrange
            var canteen = new Canteen { Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};
            _mockContext.Canteens.Add(canteen);
            _mockContext.SaveChanges();

            // Act
            var result = await _repository.DeleteAsync(canteen);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Main Canteen", result.Name);
        }
        [Fact]
        public async Task DeleteAsync_ShouldNotReturnDeletedCanteen()
        {
            var canteen = new Canteen { Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false};
            
            // act
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.DeleteAsync(canteen));
            
            // assert
            Assert.Equal("Canteen not found", exception.Result.Message);
        }
    }
}