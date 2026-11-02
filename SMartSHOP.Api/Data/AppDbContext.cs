using Microsoft.EntityFrameworkCore;
using SMartSHOP.Api.Models;

namespace SMartSHOP.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Laptopy" },
            new Category { Id = 2, Name = "Smartfony" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "MacBook Pro", Description = "M3 Chip, 16GB RAM", Price = 9999.99m, CategoryId = 1 },
            new Product { Id = 2, Name = "iPhone 15", Description = "128GB Black", Price = 4299.00m, CategoryId = 2 },
            new Product { Id = 3, Name = "Dell XPS 15", Description = "Intel i7, 32GB RAM", Price = 8500.00m, CategoryId = 1 }
    );
    }
}