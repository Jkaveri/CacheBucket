#region

using System.Data.Entity;
using WebApplication.Data.Models;

#endregion

namespace WebApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
           
        }

        public DbSet<UserPreference> UserPreferences { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<User>()
                .HasKey(t => t.Id)
                .HasMany(t => t.UserPreferences)
                .WithRequired(t => t.User)
                .HasForeignKey(t => t.UserId);

            builder.Entity<UserPreference>().HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }
    }
}