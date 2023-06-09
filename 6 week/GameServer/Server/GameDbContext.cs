using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server;

public class GameDbContext : DbContext
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<HeroesSettings> HeroesSettings { get; set; }
    public DbSet<DefaultHeroes> DefaultHeroes  { get; set; }

    public GameDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HeroesSettings>()
            .HasOne(h => h.UserProfile)
            .WithMany(u => u.HeroesSettings)
            .HasForeignKey(h => h.UserId);
        
        base.OnModelCreating(modelBuilder);
    }
}