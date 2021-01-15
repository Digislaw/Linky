using Linky.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Linky.Entities
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // DbSets

        public AppDbContext() : base("AppConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configurations
            // modelBuilder.Configurations.Add(new xyz());

            base.OnModelCreating(modelBuilder);
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
