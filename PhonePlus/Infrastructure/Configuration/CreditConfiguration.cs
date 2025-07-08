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

        builder.Property(c => c.ComercialValue)
            .IsRequired();
        
        builder.Property(c => c.NominalValue)
            .IsRequired();
        
        builder.Property(c => c.StructurationRate)
            .IsRequired(false);
        builder.Property(c => c.ColonRate)
            .IsRequired(false);
        builder.Property(c => c.FlotationRate)
            .IsRequired(false);
        builder.Property(c => c.CavaliRate)
            .IsRequired(false);
        builder.Property(c => c.PrimRate)
            .IsRequired(false);
        builder.Property(c => c.NumberOfYears)
            .IsRequired();

        builder.Property(c => c.State)
            .IsRequired();

        builder.Property(c => c.Frequencies)
            .IsRequired();
        builder.Property(c => c.DayPerYear)
            .IsRequired();
        builder.Property(c => c.CapitalizationTypes)
            .IsRequired();
        builder.Property(c => c.YearDiscount)
            .IsRequired();
        builder.Property(c => c.RentImport)
            .IsRequired();
        builder.Property(c => c.EmitionDate)
            .IsRequired();
        builder.Property(c => c.Currency)
            .IsRequired();
        builder.Property(c => c.CuponRate)
            .IsRequired();
        builder.Property(c => c.CuponRateType)
            .IsRequired();
        builder.Property(c => c.CuponRateFrequency)
            .IsRequired();
        builder.Property(c => c.CuponRateCapitalization)
            .IsRequired(false);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        builder.HasMany(c => c.GracePeriods)
            .WithOne(g => g.Credit)
            .HasForeignKey(g => g.CreditId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}