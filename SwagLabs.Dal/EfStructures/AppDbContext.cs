
using Microsoft.EntityFrameworkCore;
using SwagLabs.Models.Entities;
using SwagLabs.Models.Entities.Configuration;

namespace SwagLabs.Dal.EfStructures;

public class AppDbContext : DbContext
{
    
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<SeriLogEntry> SeriLogEntries { get; set; }
        
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {   
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserConfiguration().Configure(modelBuilder.Entity<User>());
        new SeriLogEntryConfiguration().Configure(modelBuilder.Entity<SeriLogEntry>());
    }
}
