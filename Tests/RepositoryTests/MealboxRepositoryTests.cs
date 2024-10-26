using System.Collections.ObjectModel;
using Application.Repositories;
using Infrastructure.ContextClasses;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace Tests;

public class MealboxRepositoryTests : IAsyncLifetime
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

    [Fact]
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

        var mealbox1 = new MealBox()
        {
            Id = 1, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };

        _mockContext.MealBoxes.Add(mealbox1);
        _mockContext.SaveChanges();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetMyMealBoxes_ShouldReturnmMyMealboxes()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };

        var mealbox1 = new MealBox()
        {
            Id = 1, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = student, MealType = MealType.Bread
        };

        _mockContext.MealBoxes.Add(mealbox1);
        _mockContext.SaveChanges();

        //act
        var result = await _repository.GetMyMealboxes(1);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GetAllAvailableAsync()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var mealbox1 = new MealBox()
        {
            Id = 1, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };

        _mockContext.MealBoxes.Add(mealbox1);
        _mockContext.SaveChanges();

        //act
        var result = await _repository.GetAllAvailableAsync();

        Assert.NotNull(result);
        Assert.Single(result);

    }

    [Fact]
    public async Task GetCanteenMealBoxesAsync()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var mealbox1 = new MealBox()
        {
            Id = 1, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };

        _mockContext.MealBoxes.Add(mealbox1);
        _mockContext.SaveChanges();

        //act
        var result = await _repository.GetCanteenMealboxesAsync(1);

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateMealbox()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var mealbox = new MealBox()
        {
            Id = 1, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };

        //act
        var result = await _repository.CreateAsync(mealbox);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("mealbox1", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateMealbox()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var mealbox = new MealBox()
        {
            Id = 1, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };
        var mealbox2 = new MealBox()
        {
            Id = 1, Name = "mealbox2", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };

        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();

        //act
        mealbox.Name = "mealbox2";
        var result = await _repository.UpdateAsync(mealbox2);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("mealbox2", result.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldNotUpdateMealbox()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };


        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.UpdateAsync(mealbox));

        Assert.NotNull(exception);
        Assert.Equal("MealBox not found", exception.Result.Message);

    }

    [Fact]
    public async Task UpdateAsync_ShouldNotUpdateReservedMealbox()
    {
        //Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = student, MealType = MealType.Bread
        };
        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();
        
        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.UpdateAsync(mealbox));
        
        //assert
        Assert.NotNull(exception);
        Assert.Equal("Cannot update a reserved MealBox", exception.Result.Message);
        
    }
    [Fact]
    public async Task DeleteAsync_ShouldDeleteMealbox()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var mealbox = new MealBox()
        {
            Id = 1, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };

        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();

        //act
        var result = await _repository.DeleteAsync(mealbox);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteMealbox()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };

        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.DeleteAsync(mealbox));

        Assert.NotNull(exception);
        Assert.Equal("MealBox not found", exception.Result.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotDeleteReservedMealbox()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = student, MealType = MealType.Bread
        };
        
        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();
        
        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.DeleteAsync(mealbox));
        
        //assert
        Assert.NotNull(exception);
        Assert.Equal("Cannot delete a reserved MealBox", exception.Result.Message);
        
    }

    public async Task ReservateMealBoxAsync_ShouldReservateMealBox()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };
        
        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();
        
        //act
        var result = await _repository.ReservateMealBoxAsync(5, 1);
        
        //assert
        
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(1, result.ReservedStudent.Id);
        
    }

    [Fact]
    public async Task ReservateMealBoxAsync_ShouldNotReservateMealBox()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };
        
        
        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();
        
        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.ReservateMealBoxAsync(50, 1));
        
        //assert
        Assert.NotNull(exception);
        Assert.Equal("MealBox not found", exception.Result.Message);
    }

    [Fact]
    public async Task ReservateMealBoxAsync_ShouldNotReservateMealBox2()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = student, MealType = MealType.Bread
        };

        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();

        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.ReservateMealBoxAsync(5, 1));

        //assert
        Assert.NotNull(exception);

        Assert.Equal("MealBox already reserved", exception.Result.Message);
    }

    [Fact]
    public async Task ReservateMealBoxAsync_ShouldNotReservateMealBox3()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = true, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };
        _mockContext.Students.Add(student);
        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();
        
        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.ReservateMealBoxAsync(5, 1));
        
        //assert
        Assert.NotNull(exception);
        Assert.Equal("MealBox is 18+", exception.Result.Message);
    }

    [Fact]
    public async Task ReservateMealBoxAsync_ShouldNotReservateMealBox4()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = new DateTime(2023, 10, 5, 14, 30, 0), ReservedStudent = student, MealType = MealType.Bread
        };
        var mealbox2 = new MealBox()
        {
            Id = 6, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = new DateTime(2023, 10, 5, 14, 30, 0), ReservedStudent = null, MealType = MealType.Bread
        };
        
        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.MealBoxes.Add(mealbox2);
        _mockContext.SaveChanges();
        
        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.ReservateMealBoxAsync(6, 1));
        
        //assert
        Assert.NotNull(exception);
        Assert.Equal("You already have a mealbox reserved for this pickup date.", exception.Result.Message);
        
        }
    [Fact]
    public async Task IsMealBoxReserved_ShouldReturnTrue()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = student, MealType = MealType.Bread
        };
        
        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();
        
        //act
        var result = await _repository.IsMealBoxReserved(5);
        
        //assert
        Assert.True(result);
    }

    public async Task IsMealBoxReserved_ShouldReturnFalse()
    {
        // Arrange
        var canteen = new Canteen
        {
            Id = 1, Name = "Main Canteen", Address = "lovensdijkstraat 32", City = City.Amsterdam, IsWarmFood = false
        };
        var student = new Student()
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", BirthDate = DateTime.Now,
            PhoneNumber = "02839289382", StudentNumber = 21333
        };
        var mealbox = new MealBox()
        {
            Id = 5, Name = "mealbox1", Canteen = canteen, CanteenId = 1, City = City.Amsterdam, Price = 10m,
            Products = new Collection<Product>(), EighteenPlus = false, ExpireDate = DateTime.Today,
            PickUpDate = DateTime.Today, ReservedStudent = null, MealType = MealType.Bread
        };

        _mockContext.MealBoxes.Add(mealbox);
        _mockContext.SaveChanges();

        //act
        var result = await _repository.IsMealBoxReserved(6);

        //assert
        Assert.False(result);
    }



}


