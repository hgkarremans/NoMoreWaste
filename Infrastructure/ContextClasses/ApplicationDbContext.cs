using Microsoft.EntityFrameworkCore;
using NoMoreWaste.Domain.DomainModels;
using NoMoreWaste.Domain.DomainModels.Enums;

namespace Infrastructure.ContextClasses;

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

        //STUDENTS  
        var student = new Student()
        {
            Id = 1,
            Name = "HG Karremans",
            Email = "hg@gmail.com",
            BirthDate = new DateTime(2006, 01, 01),
            PhoneNumber = "123456",
            City = City.Breda
        };
        var student2 = new Student()
        {
            Id = 2,
            Name = "Jane Doe",
            Email = "Jane@gmail.com",
            BirthDate = new DateTime(2012, 01, 01),
            PhoneNumber = "123456",
            City = City.Amsterdam
        };
        var student3 = new Student()
        {
            Id = 3,
            Name = "Jack Doe",
            Email = "Jack@gmail.com",
            BirthDate = new DateTime(2006, 01, 01),
            PhoneNumber = "123456",
            City = City.Breda
        };
        var student4 = new Student()
        {
            Id = 4,
            Name = "Jill Doe",
            Email = "Jill@gmail.com",
            BirthDate = new DateTime(2006, 01, 01),
            PhoneNumber = "123456",
            City = City.Breda
        };
        var student5 = new Student()
        {
            Id = 5,
            Name = "John Doe",
            Email = "John@gmail.com",
            BirthDate = new DateTime(2006, 01, 01),
            PhoneNumber = "123456",
            City = City.Breda
        };
        var student6 = new Student()
        {
            Id = 6,
            Name = "Jane Doe",
            Email = "Jane@gmail.com",
            BirthDate = new DateTime(2012, 01, 01),
            PhoneNumber = "123456",
            City = City.Breda
        };
        var student7 = new Student()
        {
            Id = 7,
            Name = "Jack Doe",
            Email = "Jack@gmail.com",
            BirthDate = new DateTime(2006, 01, 01),
            PhoneNumber = "123456",
            City = City.Breda
        };
        var student8 = new Student()
        {
            Id = 8,
            Name = "Jill Doe",
            Email = "Jill@gmail.com",
            BirthDate = new DateTime(2006, 01, 01),
            PhoneNumber = "123456",
            City = City.Breda
        };
        var student9 = new Student()
        {
            Id = 9,
            Name = "John Doe",
            Email = "John@gmail.com",
            BirthDate = new DateTime(2006, 01, 01),
            PhoneNumber = "123456",
            City = City.Breda
        };


        //CANTEENS


        var LA = new Canteen()
        {
            Id = 1,
            City = City.Breda,
            Address = "LA street",
            IsWarmFood = false,
            Name = "LA Canteen"
        };
        var LB = new Canteen()
        {
            Id = 2,
            City = City.Amsterdam,
            Address = "LB street",
            IsWarmFood = true,
            Name = "LB Canteen"
        };
        var LC = new Canteen()
        {
            Id = 3,
            City = City.Eindhoven,
            Address = "LC street",
            IsWarmFood = false,
            Name = "LC Canteen"
        };
        var LD = new Canteen()
        {
            Id = 4,
            City = City.Rotterdam,
            Address = "LD street",
            IsWarmFood = true,
            Name = "LD Canteen"
        };
        //CANTEENWORKERS
        var worker = new CanteenWorker()
        {
            Id = 1,
            Name = "Hg Karremans",
            Email = "hg.karremans@gmail.com",
            PersonalNumber = 123456,
            CanteenId = 1
        };
        var worker2 = new CanteenWorker()
        {
            Id = 2,
            Name = "Jane Doe",
            Email = "jane.doe@gmail.com",
            PersonalNumber = 12345,
            CanteenId = 2
        };

        //PRODUCTS

        var product1 = new Product()
        {
            Id = 1,
            Name = "apple",
            HasAlcohol = false,
            ImageUrl = "apple.jpg"
        };
        var product2 = new Product()
        {
            Id = 2,
            Name = "banana",
            HasAlcohol = false,
            ImageUrl = "banana.jpg"
        };
        var product3 = new Product()
        {
            Id = 3,
            Name = "pasta",
            HasAlcohol = false,
            ImageUrl = "pasta.jpg"
        };
        var product4 = new Product()
        {
            Id = 4,
            Name = "wine",
            HasAlcohol = true,
            ImageUrl = "wine.jpg"
        };
        var product5 = new Product()
        {
            Id = 5,
            Name = "orange",
            HasAlcohol = false,
            ImageUrl = "orange.jpg"
        };

        //MEALBOXES
        var mealBox1 = new MealBox()
        {
            Id = 1,
            Name = "Bierbox",
            Price = 10M,
            CanteenId = 1,
            PickUpDate = DateTime.Today.AddDays(4),
            ExpireDate = DateTime.Today.AddDays(10),
            City = City.Breda,
            EighteenPlus = true,
            MealType = MealType.Drink,
            Products = new List<Product>(),
            IsWarmFood = false
        };

        var mealBox2 = new MealBox()
        {
            Id = 2,
            Name = "Fruitbox",
            Price = 5M,
            CanteenId = 1,
            PickUpDate = DateTime.Today.AddDays(4),
            ExpireDate = DateTime.Today.AddDays(10),
            City = City.Breda,
            EighteenPlus = false,
            MealType = MealType.Breakfast,
            Products = new List<Product>(),
            IsWarmFood = false
        };
        var mealBox3 = new MealBox()
        {
            Id = 3,
            Name = "Wijnbox",
            Price = 15M,
            CanteenId = 1,
            PickUpDate = DateTime.Today.AddDays(4),
            ExpireDate = DateTime.Today.AddDays(10),
            City = City.Breda,
            EighteenPlus = true,
            MealType = MealType.Drink,
            Products = new List<Product>(),
            IsWarmFood = false
        };
        var mealBox4 = new MealBox()
        {
            Id = 4,
            Name = "Dinner",
            Price = 5M,
            CanteenId = 1,
            PickUpDate = DateTime.Today.AddDays(4),
            ExpireDate = DateTime.Today.AddDays(10),
            City = City.Breda,
            EighteenPlus = false,
            MealType = MealType.Breakfast,
            Products = new List<Product>(),
            IsWarmFood = false
        };
        var mealBox5 = new MealBox()
        {
            Id = 5,
            Name = "Bierbox",
            Price = 10M,
            CanteenId = 1,
            PickUpDate = DateTime.Today.AddDays(4),
            ExpireDate = DateTime.Today.AddDays(10),
            City = City.Breda,
            EighteenPlus = true,
            MealType = MealType.Drink,
            Products = new List<Product>(),
            IsWarmFood = false
        };
        var mealBox6 = new MealBox()
        {
            Id = 6,
            Name = "Fruitbox",
            Price = 5M,
            CanteenId = 1,
            PickUpDate = DateTime.Today.AddDays(4),
            ExpireDate = DateTime.Today.AddDays(10),
            City = City.Breda,
            EighteenPlus = false,
            MealType = MealType.Breakfast,
            Products = new List<Product>(),
            IsWarmFood = false
        };
        modelBuilder.Entity<Student>().HasData(student, student2, student3, student4, student5, student6, student7,
            student8, student9);
        modelBuilder.Entity<Canteen>().HasData(LA, LB, LC, LD);
        modelBuilder.Entity<Product>().HasData(product1, product2, product3, product4, product5);
        modelBuilder.Entity<MealBox>().HasData(mealBox1, mealBox2, mealBox3, mealBox4, mealBox5, mealBox6);
        modelBuilder.Entity<CanteenWorker>().HasData(worker, worker2);

        modelBuilder.Entity<MealBox>()
            .HasMany(p => p.Products)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "MealBoxProduct",
                r => r.HasOne<Product>().WithMany().HasForeignKey("ProductsId"),
                l => l.HasOne<MealBox>().WithMany().HasForeignKey("MealBoxId"),
                je =>
                {
                    je.HasKey("ProductsId", "MealBoxId");
                    je.HasData(
                        new { ProductsId = 5, MealBoxId = 1 },
                        new { ProductsId = 2, MealBoxId = 2 },
                        new { ProductsId = 4, MealBoxId = 3 },
                        new { ProductsId = 3, MealBoxId = 4 },
                        new { ProductsId = 4, MealBoxId = 4 }
                        
                    );
                });
    }
}