using DataAccess.SQL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.SQL.ApplicationDbContext
{
    public class AppDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("security");
            builder.Entity<UserEntity>(entity =>
            {
                entity.ToTable(name: "Users");
                entity.Property(i => i.PhoneNumber).IsRequired(true);
                entity.Property(i => i.Email).IsRequired(true);
            });
            builder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            builder.Entity<IdentityUserClaim<int>>(entity => { entity.ToTable(name: "UserClaims"); });
            builder.Entity<IdentityUserRole<int>>(entity => { entity.ToTable(name: "UserRoles"); });
            builder.Entity<IdentityUserLogin<int>>(entity => { entity.ToTable(name: "UserLogins"); });
            builder.Entity<IdentityUserToken<int>>(entity => { entity.ToTable(name: "UserTokens"); });
            builder.Entity<IdentityRoleClaim<int>>(entity => { entity.ToTable(name: "RoleClaims"); });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Jogging_Tracker;Trusted_Connection=true; MultipleActiveResultSets=true");
        }

        public DbSet<JoggingEntity> JoggingEntities { get; set; }
    }
}
