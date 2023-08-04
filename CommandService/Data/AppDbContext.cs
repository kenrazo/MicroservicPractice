using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Platform>()
                .HasMany(m => m.Commands)
                .WithOne(m => m.Platform!)
                .HasForeignKey(m => m.PlatformId);

            modelBuilder
                .Entity<Command>()
                .HasOne(m => m.Platform!)
                .WithMany(m => m.Commands)
                .HasForeignKey(m => m.PlatformId);
        }
    }
}