﻿// <auto-generated />
using System;
using Infrastructure.ContextClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.1.24451.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MealBoxProduct", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.Property<int>("MealBoxId")
                        .HasColumnType("int");

                    b.HasKey("ProductsId", "MealBoxId");

                    b.HasIndex("MealBoxId");

                    b.ToTable("MealBoxProduct");

                    b.HasData(
                        new
                        {
                            ProductsId = 5,
                            MealBoxId = 1
                        },
                        new
                        {
                            ProductsId = 2,
                            MealBoxId = 2
                        },
                        new
                        {
                            ProductsId = 4,
                            MealBoxId = 3
                        },
                        new
                        {
                            ProductsId = 3,
                            MealBoxId = 4
                        },
                        new
                        {
                            ProductsId = 4,
                            MealBoxId = 4
                        });
                });

            modelBuilder.Entity("NoMoreWaste.Domain.DomainModels.Canteen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<bool>("IsWarmFood")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Canteens");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "LA street",
                            City = 4,
                            IsWarmFood = false,
                            Name = "LA Canteen"
                        },
                        new
                        {
                            Id = 2,
                            Address = "LB street",
                            City = 2,
                            IsWarmFood = true,
                            Name = "LB Canteen"
                        },
                        new
                        {
                            Id = 3,
                            Address = "LC street",
                            City = 0,
                            IsWarmFood = false,
                            Name = "LC Canteen"
                        },
                        new
                        {
                            Id = 4,
                            Address = "LD street",
                            City = 1,
                            IsWarmFood = true,
                            Name = "LD Canteen"
                        });
                });

            modelBuilder.Entity("NoMoreWaste.Domain.DomainModels.CanteenWorker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CanteenId");

                    b.ToTable("CanteenWorkers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanteenId = 1,
                            Email = "hg.karremans@gmail.com",
                            Name = "Hg Karremans",
                            PersonalNumber = 123456
                        },
                        new
                        {
                            Id = 2,
                            CanteenId = 2,
                            Email = "jane.doe@gmail.com",
                            Name = "Jane Doe",
                            PersonalNumber = 12345
                        });
                });

            modelBuilder.Entity("NoMoreWaste.Domain.DomainModels.MealBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CanteenId")
                        .HasColumnType("int");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<bool>("EighteenPlus")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsWarmFood")
                        .HasColumnType("bit");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PickUpDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ReservedStudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CanteenId");

                    b.HasIndex("ReservedStudentId");

                    b.ToTable("MealBoxes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanteenId = 1,
                            City = 4,
                            EighteenPlus = true,
                            ExpireDate = new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            IsWarmFood = false,
                            MealType = 1,
                            Name = "Bierbox",
                            PickUpDate = new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 10m
                        },
                        new
                        {
                            Id = 2,
                            CanteenId = 1,
                            City = 4,
                            EighteenPlus = false,
                            ExpireDate = new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            IsWarmFood = false,
                            MealType = 2,
                            Name = "Fruitbox",
                            PickUpDate = new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 5m
                        },
                        new
                        {
                            Id = 3,
                            CanteenId = 1,
                            City = 4,
                            EighteenPlus = true,
                            ExpireDate = new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            IsWarmFood = false,
                            MealType = 1,
                            Name = "Wijnbox",
                            PickUpDate = new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 15m
                        },
                        new
                        {
                            Id = 4,
                            CanteenId = 1,
                            City = 4,
                            EighteenPlus = false,
                            ExpireDate = new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            IsWarmFood = false,
                            MealType = 2,
                            Name = "Dinner",
                            PickUpDate = new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 5m
                        },
                        new
                        {
                            Id = 5,
                            CanteenId = 1,
                            City = 4,
                            EighteenPlus = true,
                            ExpireDate = new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            IsWarmFood = false,
                            MealType = 1,
                            Name = "Bierbox",
                            PickUpDate = new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 10m
                        },
                        new
                        {
                            Id = 6,
                            CanteenId = 1,
                            City = 4,
                            EighteenPlus = false,
                            ExpireDate = new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Local),
                            IsWarmFood = false,
                            MealType = 2,
                            Name = "Fruitbox",
                            PickUpDate = new DateTime(2024, 10, 30, 0, 0, 0, 0, DateTimeKind.Local),
                            Price = 5m
                        });
                });

            modelBuilder.Entity("NoMoreWaste.Domain.DomainModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("HasAlcohol")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            HasAlcohol = false,
                            ImageUrl = "apple.jpg",
                            Name = "apple"
                        },
                        new
                        {
                            Id = 2,
                            HasAlcohol = false,
                            ImageUrl = "banana.jpg",
                            Name = "banana"
                        },
                        new
                        {
                            Id = 3,
                            HasAlcohol = false,
                            ImageUrl = "pasta.jpg",
                            Name = "pasta"
                        },
                        new
                        {
                            Id = 4,
                            HasAlcohol = true,
                            ImageUrl = "wine.jpg",
                            Name = "wine"
                        },
                        new
                        {
                            Id = 5,
                            HasAlcohol = false,
                            ImageUrl = "orange.jpg",
                            Name = "orange"
                        });
                });

            modelBuilder.Entity("NoMoreWaste.Domain.DomainModels.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("City")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 4,
                            Email = "hg@gmail.com",
                            Name = "HG Karremans",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 2,
                            Email = "Jane@gmail.com",
                            Name = "Jane Doe",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        },
                        new
                        {
                            Id = 3,
                            BirthDate = new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 4,
                            Email = "Jack@gmail.com",
                            Name = "Jack Doe",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        },
                        new
                        {
                            Id = 4,
                            BirthDate = new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 4,
                            Email = "Jill@gmail.com",
                            Name = "Jill Doe",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        },
                        new
                        {
                            Id = 5,
                            BirthDate = new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 4,
                            Email = "John@gmail.com",
                            Name = "John Doe",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        },
                        new
                        {
                            Id = 6,
                            BirthDate = new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 4,
                            Email = "Jane@gmail.com",
                            Name = "Jane Doe",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        },
                        new
                        {
                            Id = 7,
                            BirthDate = new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 4,
                            Email = "Jack@gmail.com",
                            Name = "Jack Doe",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        },
                        new
                        {
                            Id = 8,
                            BirthDate = new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 4,
                            Email = "Jill@gmail.com",
                            Name = "Jill Doe",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        },
                        new
                        {
                            Id = 9,
                            BirthDate = new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            City = 4,
                            Email = "John@gmail.com",
                            Name = "John Doe",
                            PhoneNumber = "123456",
                            StudentNumber = 0
                        });
                });

            modelBuilder.Entity("MealBoxProduct", b =>
                {
                    b.HasOne("NoMoreWaste.Domain.DomainModels.MealBox", null)
                        .WithMany()
                        .HasForeignKey("MealBoxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoMoreWaste.Domain.DomainModels.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NoMoreWaste.Domain.DomainModels.CanteenWorker", b =>
                {
                    b.HasOne("NoMoreWaste.Domain.DomainModels.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Canteen");
                });

            modelBuilder.Entity("NoMoreWaste.Domain.DomainModels.MealBox", b =>
                {
                    b.HasOne("NoMoreWaste.Domain.DomainModels.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NoMoreWaste.Domain.DomainModels.Student", "ReservedStudent")
                        .WithMany()
                        .HasForeignKey("ReservedStudentId");

                    b.Navigation("Canteen");

                    b.Navigation("ReservedStudent");
                });
#pragma warning restore 612, 618
        }
    }
}
