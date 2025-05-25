using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhonePlus.Domain.Models;

namespace PhonePlus.Infrastructure.Configuration;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .ValueGeneratedOnAdd();

        builder.Property(n => n.Title)
            .IsRequired();

        builder.Property(n => n.Description)
            .IsRequired();

        builder.HasOne<User>().WithMany().HasForeignKey(n => n.UserId);
    }
}