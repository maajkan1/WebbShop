using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebbShopApi.Models;

namespace WebbShopApi.Data.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> user)
    {
        user.HasKey(u => u.Id);
        user.Property(u => u.Id).ValueGeneratedOnAdd();
        
        user.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        //PasswordHash is defined with DataAnnotations.
        
        user.Property(u => u.Email)
            .IsRequired();
        user.HasIndex(u => u.Email)
            .IsUnique();
        
        user.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
    }
}
