using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;

namespace Application.ContextClasses;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<MealBox> MealBoxes { get; set; }
    public virtual DbSet<Canteen> Canteens { get; set; }
    public virtual DbSet<CanteenWorker> CanteenWorkers { get; set; }
    public virtual DbSet<Product> Products {}
    
    
}