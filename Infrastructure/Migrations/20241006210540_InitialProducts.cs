using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canteens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsWarmFood = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canteens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CanteenWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalNumber = table.Column<int>(type: "int", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanteenWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CanteenWorkers_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<int>(type: "int", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false),
                    PickUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EighteenPlus = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    ReservedStudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealBoxes_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealBoxes_Students_ReservedStudentId",
                        column: x => x.ReservedStudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MealBoxProduct",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    MealBoxId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealBoxProduct", x => new { x.ProductsId, x.MealBoxId });
                    table.ForeignKey(
                        name: "FK_MealBoxProduct_MealBoxes_MealBoxId",
                        column: x => x.MealBoxId,
                        principalTable: "MealBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealBoxProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "Address", "City", "IsWarmFood", "Name" },
                values: new object[,]
                {
                    { 1, "LA street", 4, false, "LA Canteen" },
                    { 2, "LB street", 2, true, "LB Canteen" },
                    { 3, "LC street", 0, false, "LC Canteen" },
                    { 4, "LD street", 1, true, "LD Canteen" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "HasAlcohol", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, false, "apple.jpg", "apple" },
                    { 2, false, "banana.jpg", "banana" },
                    { 3, true, "beer.jpg", "beer" },
                    { 4, true, "wine.jpg", "wine" },
                    { 5, false, "orange.jpg", "orange" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "Email", "Name", "PhoneNumber", "StudentNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hg@gmail.com", "HG Karremans", "123456", 0 },
                    { 2, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane.gmail.com", "Jane Doe", "123456", 0 },
                    { 3, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jack.gmail.com", "Jack Doe", "123456", 0 },
                    { 4, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jill.gmail.com", "Jill Doe", "123456", 0 },
                    { 5, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John.gmail.com", "John Doe", "123456", 0 },
                    { 6, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane.gmail.com", "Jane Doe", "123456", 0 },
                    { 7, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jack.gmail.com", "Jack Doe", "123456", 0 },
                    { 8, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jill.gmail.com", "Jill Doe", "123456", 0 },
                    { 9, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John.gmail.com", "John Doe", "123456", 0 },
                    { 10, new DateTime(2006, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane.gmail.com", "Jane Doe", "123456", 0 }
                });

            migrationBuilder.InsertData(
                table: "CanteenWorkers",
                columns: new[] { "Id", "CanteenId", "Email", "Name", "PersonalNumber" },
                values: new object[] { 1, 1, "hg.karremans@gmail.com", "Hg Karremans", 123456 });

            migrationBuilder.InsertData(
                table: "MealBoxes",
                columns: new[] { "Id", "CanteenId", "City", "EighteenPlus", "ExpireDate", "MealType", "Name", "PickUpDate", "Price", "ReservedStudentId" },
                values: new object[,]
                {
                    { 1, 1, 4, true, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Local), 1, "Bierbox", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 10m, null },
                    { 2, 1, 4, false, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Local), 2, "Fruitbox", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 5m, null },
                    { 3, 1, 4, true, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Local), 1, "Wijnbox", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 15m, null },
                    { 4, 1, 4, false, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Local), 2, "Fruitbox", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 5m, null },
                    { 5, 1, 4, true, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Local), 1, "Bierbox", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 10m, null },
                    { 6, 1, 4, false, new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Local), 2, "Fruitbox", new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Local), 5m, null }
                });

            migrationBuilder.InsertData(
                table: "MealBoxProduct",
                columns: new[] { "MealBoxId", "ProductsId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 2, 2 },
                    { 3, 4 },
                    { 1, 5 },
                    { 4, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanteenWorkers_CanteenId",
                table: "CanteenWorkers",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_MealBoxes_CanteenId",
                table: "MealBoxes",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_MealBoxes_ReservedStudentId",
                table: "MealBoxes",
                column: "ReservedStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MealBoxProduct_MealBoxId",
                table: "MealBoxProduct",
                column: "MealBoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanteenWorkers");

            migrationBuilder.DropTable(
                name: "MealBoxProduct");

            migrationBuilder.DropTable(
                name: "MealBoxes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Canteens");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
