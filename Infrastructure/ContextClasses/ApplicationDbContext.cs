using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;

namespace Application.ContextClasses;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<MealBox> MealBoxes { get; set; }
    public virtual DbSet<Canteen> Canteens { get; set; }
    public virtual DbSet<CanteenWorker> CanteenWorkers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var student = new Student()
        {
            Id = 1,
            Name = "John Doe",
            Email = "John.gmail.com",
            //should be atleast be 16 years old
            BirthDate = new DateTime(2006, 01, 01),
            PhoneNumber = "123456"
        };
        //add way more data
        
        
    }
    
    
    
    
}