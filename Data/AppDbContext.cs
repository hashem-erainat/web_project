using Microsoft.EntityFrameworkCore;
using project_18.Models;

namespace project_18.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
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
                    FullName = "BalaMarket Admin",
                    Role = "Admin"
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Vintage Electronics" },
                new Category { CategoryId = 2, Name = "Collectibles & Art" },
                new Category { CategoryId = 3, Name = "Home & Living" },
                new Category { CategoryId = 4, Name = "Fashion & Accessories" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    CategoryId = 1,
                    Name = "Retro Film Camera",
                    Price = 320.00m,
                    Description = "A classic 35mm film camera from the 1980s. Excellent aesthetic condition and fully functional shutter.",
                    ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 1, 10, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 2,
                    CategoryId = 4,
                    Name = "Mechanical Pocket Watch",
                    Price = 180.00m,
                    Description = "Vintage hand-wound mechanical pocket watch with elegant engravement. Keeps time perfectly.",
                    ImageUrl = "https://images.unsplash.com/photo-1509048191080-d2984bad6ae5?w=500",
                    Condition = "Excellent",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 2, 11, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 3,
                    CategoryId = 1,
                    Name = "Vintage Typewriter",
                    Price = 450.00m,
                    Description = "Authentic manual typewriter from the 1970s. All keys strike smoothly. Perfect for writers and collectors.",
                    ImageUrl = "https://images.unsplash.com/photo-1519337265831-281ec6cc8514?w=500",
                    Condition = "Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 3, 12, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 4,
                    CategoryId = 4,
                    Name = "Classic Leather Boots",
                    Price = 150.00m,
                    Description = "Genuine brown leather boots, size 42. Nicely broken-in with standard vintage character.",
                    ImageUrl = "https://images.unsplash.com/photo-1520639888713-7851133b1ed0?w=500",
                    Condition = "Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 4, 13, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 5,
                    CategoryId = 3,
                    Name = "Wooden Rocking Chair",
                    Price = 280.00m,
                    Description = "Handcrafted solid oak rocking chair. Very comfortable, sturdy frame with minor scratches on armrests.",
                    ImageUrl = "https://images.unsplash.com/photo-1592078615290-033ee584e267?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 5, 14, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 6,
                    CategoryId = 1,
                    Name = "Vinyl Record Player",
                    Price = 600.00m,
                    Description = "High-fidelity vintage record player turntable. Built-in stereo speakers and clean retro wood casing.",
                    ImageUrl = "https://images.unsplash.com/photo-1539707132456-a3c44ac79743?w=500",
                    Condition = "Excellent",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 6, 15, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 7,
                    CategoryId = 4,
                    Name = "Polarized Retro Sunglasses",
                    Price = 90.00m,
                    Description = "Classic style sunglasses with a gold metal frame and dark green polarized lenses in great shape.",
                    ImageUrl = "https://images.unsplash.com/photo-1511499767150-a48a237f0083?w=500",
                    Condition = "Excellent",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 7, 16, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 8,
                    CategoryId = 2,
                    Name = "Acoustic Folk Guitar",
                    Price = 380.00m,
                    Description = "Classic six-string acoustic guitar with a warm resonant sound. Includes original gig bag.",
                    ImageUrl = "https://images.unsplash.com/photo-1510915361894-db8b60106cb1?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 8, 17, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 9,
                    CategoryId = 4,
                    Name = "Classic Leather Jacket",
                    Price = 240.00m,
                    Description = "Vintage black leather biker jacket, size L. Durable heavy leather with nice distressed edges.",
                    ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=500",
                    Condition = "Very Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 9, 18, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    ProductId = 10,
                    CategoryId = 3,
                    Name = "Antique Brass Table Lamp",
                    Price = 190.00m,
                    Description = "Mid-century brass table lamp with a green glass banker-style shade. Tested and works perfectly.",
                    ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=500",
                    Condition = "Good",
                    IsSold = false,
                    CreatedAt = new DateTime(2026, 6, 10, 19, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
