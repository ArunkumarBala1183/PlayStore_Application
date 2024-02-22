using Microsoft.EntityFrameworkCore;
using Playstore.Contracts.Data.Entities;
using Playstore.Migrations.DateConvertor;

namespace Playstore.Migrations
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>().AsEnumerable())
            {
                item.Entity.AddedOn = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<App> App { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCredentials> UserCredentials { get; set; }
        public DbSet<AppInfo> AppInfo { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppImages> AppImages { get; set; }
        public DbSet<AppData> AppDatas { get; set; }
        public DbSet<AppReview> AppReviews { get; set; }
        public DbSet<AppDownloads> AppDownloads { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AdminRequests> AdminRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>()
            .HasMany(role => role.UserRoles)
            .WithOne(user => user.User)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Role>()
            .HasMany(user => user.UserRoles)
            .WithOne(role => role.Role)
            .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConvertor>()
            .HaveColumnType("date");
            
            base.ConfigureConventions(builder);
        }
    }
}