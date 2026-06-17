using Microsoft.EntityFrameworkCore;
using project_18.Models;

namespace project_18.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            var projectDir = Directory.GetCurrentDirectory();
            AppDomain.CurrentDomain.SetData("DataDirectory", projectDir);
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "admin",
                    Password = "admin123",
                    FullName = "Mostamal Market Admin",
                    Role = "Admin"
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronic Devices" },
                new Category { CategoryId = 2, Name = "Home Appliances" },
                new Category { CategoryId = 3, Name = "Furniture" },
                new Category { CategoryId = 4, Name = "Clothing" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    CategoryId = 1,
                    Name = "Used iPhone 13 Pro (128GB) - Graphite",
                    Price = 1600.00m,
                    Description = "Slight scratches on the sides, but the screen is in perfect condition. Battery health is at 86%. Includes original box and charging cable.",
                    ImageUrl = "https://images.unsplash.com/photo-1510557880182-3d4d3cba35a5?w=500",
                    Condition = "Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 2,
                    CategoryId = 1,
                    Name = "Dell UltraSharp 27\" Monitor (U2719D)",
                    Price = 550.00m,
                    Description = "2K resolution (2560x1440) IPS panel. No dead pixels. Great for office work or gaming. Power cable and HDMI included.",
                    ImageUrl = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 2, 11, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 3,
                    CategoryId = 1,
                    Name = "Sony WH-1000XM4 Wireless Headphones",
                    Price = 480.00m,
                    Description = "Active noise-canceling headphones in black. Ear cups show minor signs of wear but sound quality is superb. Case and audio jack included.",
                    ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 3, 12, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 4,
                    CategoryId = 2,
                    Name = "Philips Air Fryer XXL",
                    Price = 350.00m,
                    Description = "Only used a few times, extremely clean inside. Perfect for healthy cooking. Temperature dial and timer work flawlessly.",
                    ImageUrl = "https://images.unsplash.com/photo-1621972750749-0fbb1abb7736?w=500",
                    Condition = "Excellent",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 4, 13, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 5,
                    CategoryId = 2,
                    Name = "Nespresso Lattissima Touch Coffee Machine",
                    Price = 450.00m,
                    Description = "One-touch milk system coffee maker. Fully descaled and ready to use. Makes amazing lattes and cappuccinos.",
                    ImageUrl = "https://images.unsplash.com/photo-1579888944880-d98341148733?w=500",
                    Condition = "Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 5, 14, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 6,
                    CategoryId = 3,
                    Name = "Modern Fabric Loveseat Sofa",
                    Price = 750.00m,
                    Description = "Grey 2-seater sofa in modern style. Fabric has no tears or stains. Ideal for small apartments or living rooms.",
                    ImageUrl = "https://images.unsplash.com/photo-1555041469-a586c61ea9bc?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 6, 15, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 7,
                    CategoryId = 3,
                    Name = "Solid Wood Study Desk",
                    Price = 280.00m,
                    Description = "Sturdy wooden desk with 3 drawers. Perfect for students or home office. Minor surface scuffs on the top.",
                    ImageUrl = "https://images.unsplash.com/photo-1518455027359-f3f8164ba6bd?w=500",
                    Condition = "Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 7, 16, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 8,
                    CategoryId = 3,
                    Name = "Adjustable Ergonomic Office Chair",
                    Price = 200.00m,
                    Description = "Black mesh high-back chair with lumbar support. Height adjustable and tilt tension control work perfectly.",
                    ImageUrl = "https://images.unsplash.com/photo-1505797149-43b0069ec26b?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 8, 17, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 9,
                    CategoryId = 4,
                    Name = "Men's Levi's 511 Slim Fit Jeans",
                    Price = 90.00m,
                    Description = "Size 32/32, dark blue wash. Very clean, no rips or discoloration. Classic slim fit denim.",
                    ImageUrl = "https://images.unsplash.com/photo-1542272604-787c3835535d?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 9, 18, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 10,
                    CategoryId = 4,
                    Name = "North Face Waterproof Windbreaker Jacket",
                    Price = 180.00m,
                    Description = "Size M, black windbreaker. Waterproof material still works perfectly. Zipper and hood adjustments are intact.",
                    ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=500",
                    Condition = "Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 10, 19, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
