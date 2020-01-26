using Microsoft.EntityFrameworkCore;
using SwimmingPool.Domain.UserAggregate;

namespace SwimmingPool.Data
{
    public class SwimmingPoolContext : DbContext
    {
        public SwimmingPoolContext(DbContextOptions<SwimmingPoolContext> options)
           : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(user =>
            {
                user.ToTable("user").HasKey(u => u.Id);
                user.HasOne(u => u.AccountVerification).WithOne(av => av.User);
            });

            builder.Entity<AccountVerification>(accountVerification =>
            {
                accountVerification.HasKey(av => av.Key);
            });
        }
    }
}