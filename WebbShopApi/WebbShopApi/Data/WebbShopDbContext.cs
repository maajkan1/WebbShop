using Microsoft.EntityFrameworkCore;
using WebbShopApi.Data.Configurations;
using WebbShopApi.Models;

public class WebbShopDbContext(DbContextOptions<WebbShopDbContext> options) : DbContext(options)
{
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
       base.OnModelCreating(modelBuilder);
       
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebbShopDbContext).Assembly);
   }
}
