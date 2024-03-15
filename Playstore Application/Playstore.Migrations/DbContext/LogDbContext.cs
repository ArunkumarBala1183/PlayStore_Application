using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;

namespace Playstore.Migrations.Scaffold
{
    public partial class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        public virtual DbSet<AppLog> AppLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppLog>(entity =>
            {
                // entity.Property(e => e.Location).IsUnicode(false);

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });
        }
    }
}
