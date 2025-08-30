using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SwagLabs.Models.Entities.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Price)
            .IsRequired();

        builder.ToTable(b => b.IsTemporal(t => 
        {
            t.HasPeriodEnd("ValidTo");
            t.HasPeriodStart("ValidFrom");
            t.UseHistoryTable($"{nameof(Product)}Audit");
        }));
    }
}
