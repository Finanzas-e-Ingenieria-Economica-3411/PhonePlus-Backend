using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhonePlus.Domain.Models;

namespace PhonePlus.Infrastructure.Configuration;

public sealed class CreditConfiguration : IEntityTypeConfiguration<Credit>
{
    public void Configure(EntityTypeBuilder<Credit> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.PhoneNumber)
            .IsRequired();

        builder.Property(c => c.Price)
            .IsRequired();

        builder.Property(c => c.StartDate)
            .IsRequired();

        builder.Property(c => c.Months)
            .IsRequired();

        builder.Property(c => c.InterestRate)
            .IsRequired();

        builder.Property(c => c.Insurance)
            .IsRequired();

        builder.Property(c => c.Amortization)
            .IsRequired();

        builder.Property(c => c.Paid)
            .IsRequired();

        builder.Property(c => c.Interest)
            .IsRequired();

        builder.Property(c => c.PendingPayment)
            .IsRequired();

        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);

        builder.HasOne<State>()
            .WithMany()
            .HasForeignKey(x => x.StateId);
    }
}