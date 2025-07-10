
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SwagLabs.Models.Entities.Configuration;

public class SeriLogEntryConfiguration : IEntityTypeConfiguration<SeriLogEntry>
{
    public void Configure(EntityTypeBuilder<SeriLogEntry> builder)
    {
        builder.ToTable(nameof(SeriLogEntry));

        builder.Property(e => e.Properties).HasColumnType("Xml");

        builder.Property(e => e.TimeStamp).HasDefaultValueSql("GetDate()");

    }
}
