using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_18.Migrations
{
    public partial class UpdateSeededProductsAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Name",
                value: "Electronic Devices");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Name",
                value: "Home Appliances");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Name",
                value: "Furniture");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "Name",
                value: "Clothing");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Good", "Slight scratches on the sides, but the screen is in perfect condition. Battery health is at 86%. Includes original box and charging cable.", "https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?w=500", "Used iPhone 13 Pro (128GB) - Graphite", 450.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 1, "Very Good", "2K resolution (2560x1440) IPS panel. No dead pixels. Great for office work or gaming. Power cable and HDMI included.", "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=500", "Dell UltraSharp 27\" Monitor (U2719D)", 120.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Very Good", "Active noise-canceling headphones in black. Ear cups show minor signs of wear but sound quality is superb. Case and audio jack included.", "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500", "Sony WH-1000XM4 Wireless Headphones", 140.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 2, "Excellent", "Only used a few times, extremely clean inside. Perfect for healthy cooking. Temperature dial and timer work flawlessly.", "https://images.unsplash.com/photo-1621972750749-0fbb1abb7736?w=500", "Philips Air Fryer XXL", 75.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 2, "Good", "One-touch milk system coffee maker. Fully descaled and ready to use. Makes amazing lattes and cappuccinos.", "https://images.unsplash.com/photo-1579888944880-d98341148733?w=500", "Nespresso Lattissima Touch Coffee Machine", 110.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 3, "Very Good", "Grey 2-seater sofa in modern style. Fabric has no tears or stains. Ideal for small apartments or living rooms.", "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=500", "Modern Fabric Loveseat Sofa", 220.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 3, "Good", "Sturdy wooden desk with 3 drawers. Perfect for students or home office. Minor surface scuffs on the top.", "https://images.unsplash.com/photo-1518455027359-f3f8164ba6bd?w=500", "Solid Wood Study Desk", 95.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 3, "Black mesh high-back chair with lumbar support. Height adjustable and tilt tension control work perfectly.", "https://images.unsplash.com/photo-1505797149-43b0069ec26b?w=500", "Adjustable Ergonomic Office Chair", 65.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Size 32/32, dark blue wash. Very clean, no rips or discoloration. Classic slim fit denim.", "https://images.unsplash.com/photo-1542272604-787c3835535d?w=500", "Men's Levi's 511 Slim Fit Jeans", 25.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 4, "Size M, black windbreaker. Waterproof material still works perfectly. Zipper and hood adjustments are intact.", "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=500", "North Face Waterproof Windbreaker Jacket", 48.00m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "Name",
                value: "Vintage Electronics");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "Name",
                value: "Collectibles & Art");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "Name",
                value: "Home & Living");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "Name",
                value: "Fashion & Accessories");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                columns: new[] { "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Very Good", "A classic 35mm film camera from the 1980s. Excellent aesthetic condition and fully functional shutter.", "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=500", "Retro Film Camera", 320.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 4, "Excellent", "Vintage hand-wound mechanical pocket watch with elegant engravement. Keeps time perfectly.", "https://images.unsplash.com/photo-1509048191080-d2984bad6ae5?w=500", "Mechanical Pocket Watch", 180.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Good", "Authentic manual typewriter from the 1970s. All keys strike smoothly. Perfect for writers and collectors.", "https://images.unsplash.com/photo-1519337265831-281ec6cc8514?w=500", "Vintage Typewriter", 450.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 4, "Good", "Genuine brown leather boots, size 42. Nicely broken-in with standard vintage character.", "https://images.unsplash.com/photo-1520639888713-7851133b1ed0?w=500", "Classic Leather Boots", 150.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 3, "Very Good", "Handcrafted solid oak rocking chair. Very comfortable, sturdy frame with minor scratches on armrests.", "https://images.unsplash.com/photo-1592078615290-033ee584e267?w=500", "Wooden Rocking Chair", 280.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 1, "Excellent", "High-fidelity vintage record player turntable. Built-in stereo speakers and clean retro wood casing.", "https://images.unsplash.com/photo-1539707132456-a3c44ac79743?w=500", "Vinyl Record Player", 600.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                columns: new[] { "CategoryId", "Condition", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 4, "Excellent", "Classic style sunglasses with a gold metal frame and dark green polarized lenses in great shape.", "https://images.unsplash.com/photo-1511499767150-a48a237f0083?w=500", "Polarized Retro Sunglasses", 90.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 2, "Classic six-string acoustic guitar with a warm resonant sound. Includes original gig bag.", "https://images.unsplash.com/photo-1510915361894-db8b60106cb1?w=500", "Acoustic Folk Guitar", 380.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                columns: new[] { "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { "Vintage black leather biker jacket, size L. Durable heavy leather with nice distressed edges.", "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=500", "Classic Leather Jacket", 240.00m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { 3, "Mid-century brass table lamp with a green glass banker-style shade. Tested and works perfectly.", "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=500", "Antique Brass Table Lamp", 190.00m });
        }
    }
}
