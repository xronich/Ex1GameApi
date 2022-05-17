using Ex1Game.DataBase.Configuration;
using Ex1Game.DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ex1Game.DataBase
{
    public class PlayerDBContext : DbContext
    {
        public PlayerDBContext(DbContextOptions<PlayerDBContext> options) : base(options)
        { }

        public DbSet<Player> Player { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());

            modelBuilder.Entity<Player>()
                        .HasIndex(e => e.Id)
                        .IsUnique()
                        .IsClustered();
        }
    }
}
