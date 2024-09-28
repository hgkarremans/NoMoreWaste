
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ContextClasses;

public class NoMoreWasteIdentityDbContext : IdentityDbContext
{
    public NoMoreWasteIdentityDbContext(DbContextOptions<NoMoreWasteIdentityDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            //give totally random id
            Id = "1", Name = "employee", NormalizedName = "EMPLOYEE"
        });
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            //give totally random id
            Id = "2", Name = "student", NormalizedName = "STUDENT".ToUpper()
        });
        var hasher = new PasswordHasher<IdentityUser>();
        var studentId = Guid.NewGuid().ToString();
        modelBuilder.Entity<IdentityUser>().HasData( 
            new IdentityUser
            {
                Id = "8e445865-a24d-4523-a6c6-9443d048cdb9",
                UserName = "hg.karremans@gmail.com",
                NormalizedUserName = "HG.KARREMANS@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "test1234")
            },
            new IdentityUser
            {
                Id = studentId,
                UserName = "hg@gmail.com",
                NormalizedUserName = "HG@GMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "test1234")
            });
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                RoleId = "1",
                UserId = "8e445865-a24d-4523-a6c6-9443d048cdb9"     
            }, new IdentityUserRole<string>()
            {
                RoleId = "2",
                UserId = studentId
            });
    }
}