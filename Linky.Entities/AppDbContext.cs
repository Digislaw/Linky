using Linky.Entities.Configurations;
using Linky.Entities.Identity;
using Linky.Entities.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Linky.Entities
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Link> Links { get; set; }

        public AppDbContext() : base("AppConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configurations
            modelBuilder.Configurations.Add(new LinkConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
