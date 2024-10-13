using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NoMoreWaste.Domain.DomainModels;
using NoMoreWaste.Domain.DomainModels.Enums;
using NSubstitute;
using Xunit;

namespace Tests
{
    public class CanteenRepositoryTest
    {
        private readonly ApplicationDbContext _mockContext;
        private readonly DbSet<Canteen> _mockCanteenSet;
        private readonly CanteenRepository _repository;

        public CanteenRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _mockContext = new ApplicationDbContext(options);
            _repository = new CanteenRepository(_mockContext);
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
    }
}