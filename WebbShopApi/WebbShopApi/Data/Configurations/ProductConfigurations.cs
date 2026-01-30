using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebbShopApi.Models;

namespace WebbShopApi.Data.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> product)
    {
        product.HasKey(p => p.Id);
        
        product.Property(p => p.ProductName)
            .IsRequired()
            .HasMaxLength(50);
        
        product.Property(p => p.ProductDescription)
            .IsRequired()
            .HasMaxLength(500);
        
        product.Property(p => p.ProductPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        //Range is defined in Data-annotations.
        product.Property(p => p.Quantity)
            .IsRequired();
        
        product.HasMany(p => p.ProductCategories)
            .WithOne(pc => pc.Product)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
