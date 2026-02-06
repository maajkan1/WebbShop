using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebbShopApi.Models;

namespace WebbShopApi.Data.Configurations;

public class CartConfigurations: IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> cart)
    {
        cart.HasKey(c => c.Id);
        
        cart.HasOne(c => c.User)
            .WithOne(u => u.ActiveCart)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}