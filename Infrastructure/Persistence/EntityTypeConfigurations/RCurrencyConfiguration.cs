using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations;

public class RCurrencyConfiguration : IEntityTypeConfiguration<RCurrency>
{
    public void Configure(EntityTypeBuilder<RCurrency> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.A_Date)
            .HasColumnType("timestamp with time zone");

    }
}