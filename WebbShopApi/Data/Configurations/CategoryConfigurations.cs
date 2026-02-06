using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebbShopApi.Models;

namespace WebbShopApi.Data.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> category)
    {
        category.HasKey(u => u.Id);
        
        category.Property(u => u.CategoryName).IsRequired().HasMaxLength(50);
        category.HasIndex(u => u.CategoryName)
            .IsUnique();
        
        category.Property(u => u.CategoryDescription).IsRequired().HasMaxLength(500);
       
        category.HasMany(u => u.ProductCategories)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
