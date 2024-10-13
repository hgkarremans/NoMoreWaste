using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NoMoreWaste.Domain.DomainModels;
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

            _mockContext = Substitute.For<ApplicationDbContext>(options);
            _mockCanteenSet = Substitute.For<DbSet<Canteen>>();

            // Setup the repository
            _repository = new CanteenRepository(_mockContext);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCanteen_WhenCanteenExists()
        {
            // Arrange
            var canteen = new Canteen { Id = 1, Name = "Main Canteen" };
            var data = new List<Canteen> { canteen }.AsQueryable();

            // Setup mock DbSet
            var queryableCanteenSet = (IQueryable<Canteen>)_mockCanteenSet;
            queryableCanteenSet.Provider.Returns(data.Provider);
            queryableCanteenSet.Expression.Returns(data.Expression);
            queryableCanteenSet.ElementType.Returns(data.ElementType);
            queryableCanteenSet.GetEnumerator().Returns(data.GetEnumerator());

            _mockContext.Canteens.Returns(_mockCanteenSet);

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Main Canteen", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenCanteenNotFound()
        {
            // Arrange
            _mockContext.Canteens.Returns(_mockCanteenSet);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _repository.GetByIdAsync(99));
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewCanteen()
        {
            // Arrange
            var canteen = new Canteen { Id = 2, Name = "New Canteen" };
            var mockEntityEntry = Substitute.For<EntityEntry<Canteen>>();

            // Setup mock for AddAsync
            _mockCanteenSet.AddAsync(canteen, default).Returns(new ValueTask<EntityEntry<Canteen>>(mockEntityEntry));
            _mockContext.Canteens.Returns(_mockCanteenSet);

            // Act
            var result = await _repository.CreateAsync(canteen);

            // Assert
            await _mockCanteenSet.Received(1).AddAsync(canteen, default);
            await _mockContext.Received(1).SaveChangesAsync(default);
            Assert.Equal(canteen, result);
        }

        // Similarly, you can write tests for UpdateAsync, DeleteAsync, and GetAllAsync
    }
}