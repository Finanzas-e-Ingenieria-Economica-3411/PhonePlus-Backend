using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhonePlus.Domain.Models;

namespace PhonePlus.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Dni)
            .HasMaxLength(8)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .IsRequired();
        builder.Property(x => x.Username)
            .IsRequired();
        
        builder.Property(x => x.Password)
            .IsRequired();

        builder.HasOne<UserRole>()
            .WithMany().HasForeignKey(x => x.RoleId);



    }
}