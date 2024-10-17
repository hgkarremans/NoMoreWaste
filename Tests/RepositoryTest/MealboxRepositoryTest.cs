using Application.Repositories;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;

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
    
}