using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_18.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsSold = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Vintage Electronics" },
                    { 2, "Collectibles & Art" },
                    { 3, "Home & Living" },
                    { 4, "Fashion & Accessories" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "Password", "Role", "Username" },
                values: new object[] { 1, "Mostamal Market Admin", "admin123", "Admin", "admin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Condition", "CreatedAt", "Description", "ImageUrl", "IsSold", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Very Good", new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Utc), "A classic 35mm film camera from the 1980s. Excellent aesthetic condition and fully functional shutter.", "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=500", false, "Retro Film Camera", 320.00m },
                    { 2, 4, "Excellent", new DateTime(2026, 6, 2, 11, 0, 0, 0, DateTimeKind.Utc), "Vintage hand-wound mechanical pocket watch with elegant engravement. Keeps time perfectly.", "https://images.unsplash.com/photo-1509048191080-d2984bad6ae5?w=500", false, "Mechanical Pocket Watch", 180.00m },
                    { 3, 1, "Good", new DateTime(2026, 6, 3, 12, 0, 0, 0, DateTimeKind.Utc), "Authentic manual typewriter from the 1970s. All keys strike smoothly. Perfect for writers and collectors.", "https://images.unsplash.com/photo-1519337265831-281ec6cc8514?w=500", false, "Vintage Typewriter", 450.00m },
                    { 4, 4, "Good", new DateTime(2026, 6, 4, 13, 0, 0, 0, DateTimeKind.Utc), "Genuine brown leather boots, size 42. Nicely broken-in with standard vintage character.", "https://images.unsplash.com/photo-1520639888713-7851133b1ed0?w=500", false, "Classic Leather Boots", 150.00m },
                    { 5, 3, "Very Good", new DateTime(2026, 6, 5, 14, 0, 0, 0, DateTimeKind.Utc), "Handcrafted solid oak rocking chair. Very comfortable, sturdy frame with minor scratches on armrests.", "https://images.unsplash.com/photo-1592078615290-033ee584e267?w=500", false, "Wooden Rocking Chair", 280.00m },
                    { 6, 1, "Excellent", new DateTime(2026, 6, 6, 15, 0, 0, 0, DateTimeKind.Utc), "High-fidelity vintage record player turntable. Built-in stereo speakers and clean retro wood casing.", "https://images.unsplash.com/photo-1539707132456-a3c44ac79743?w=500", false, "Vinyl Record Player", 600.00m },
                    { 7, 4, "Excellent", new DateTime(2026, 6, 7, 16, 0, 0, 0, DateTimeKind.Utc), "Classic style sunglasses with a gold metal frame and dark green polarized lenses in great shape.", "https://images.unsplash.com/photo-1511499767150-a48a237f0083?w=500", false, "Polarized Retro Sunglasses", 90.00m },
                    { 8, 2, "Very Good", new DateTime(2026, 6, 8, 17, 0, 0, 0, DateTimeKind.Utc), "Classic six-string acoustic guitar with a warm resonant sound. Includes original gig bag.", "https://images.unsplash.com/photo-1510915361894-db8b60106cb1?w=500", false, "Acoustic Folk Guitar", 380.00m },
                    { 9, 4, "Very Good", new DateTime(2026, 6, 9, 18, 0, 0, 0, DateTimeKind.Utc), "Vintage black leather biker jacket, size L. Durable heavy leather with nice distressed edges.", "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=500", false, "Classic Leather Jacket", 240.00m },
                    { 10, 3, "Good", new DateTime(2026, 6, 10, 19, 0, 0, 0, DateTimeKind.Utc), "Mid-century brass table lamp with a green glass banker-style shade. Tested and works perfectly.", "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=500", false, "Antique Brass Table Lamp", 190.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
