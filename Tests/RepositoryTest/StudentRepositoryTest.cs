using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;

namespace Tests;

public class StudentRepositoryTest : IAsyncLifetime
{

    private ApplicationDbContext _mockContext;
    private StudentRepository _repository;

    public async Task InitializeAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _mockContext = new ApplicationDbContext(options);
        _repository = new StudentRepository(_mockContext);
    }

    public async Task DisposeAsync()
    {
        await _mockContext.DisposeAsync();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnStudent_WhenStudentExists()
    {
        // Arrange
        var student = new Student
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", PhoneNumber = "0612345678",
            BirthDate = DateTime.Now, StudentNumber = 23133
        };
        _mockContext.Students.Add(student);
        _mockContext.SaveChanges();

        // Act
        var result = await _repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenStudentDoesNotExist()
    {
        //arrange
        var student = new Student
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", PhoneNumber = "0612345678",
            BirthDate = DateTime.Now, StudentNumber = 23133
        };
        _mockContext.Students.Add(student);
        _mockContext.SaveChanges();
        
        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.GetByIdAsync(2));
        
        //assert
        Assert.Equal("Student not found", exception.Result.Message);
    }

    [Fact]
    public async Task GetByEmailAsync_ShouldReturnStudent_WhenStudentExists()
    {
        // Arrange
        var student = new Student
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", PhoneNumber = "0612345678",
            BirthDate = DateTime.Now, StudentNumber = 23133
        };
        _mockContext.Students.Add(student);
        _mockContext.SaveChanges();
        
        //act
        var result = await _repository.GetByEmailAsync("john.doe@gmail.com");
        
        //assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.Name);
    }

    [Fact]
    public async Task GetByEmailAsync_shouldThrowError_WhenStudentDoesNotExist()
    {
        var student = new Student
        {
            Id = 1, Name = "John Doe", Email = "john.doe@gmail.com", PhoneNumber = "0612345678",
            BirthDate = DateTime.Now, StudentNumber = 23133
        };
        _mockContext.Students.Add(student);
        _mockContext.SaveChanges();
        
        //act
        var exception = Assert.ThrowsAsync<Exception>(() => _repository.GetByEmailAsync("ava.lovelace@gmail.com"));
        
        
    }

}
        