using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebbShopApi.Models;

namespace WebbShopApi.Data.Configurations;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> orderItem)
    {
        orderItem.HasKey(oi => oi.Id);
        
        orderItem.Property(oi => oi.ProductName).IsRequired();
        
        orderItem.Property(oi => oi.UnitPrice).IsRequired()
            .HasColumnType("decimal(18,2)");
        
        orderItem.Property(oi => oi.Quantity).IsRequired();
        
        orderItem.HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
    }
}
