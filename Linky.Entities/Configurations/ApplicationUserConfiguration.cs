using Linky.Entities.Identity;
using System.Data.Entity.ModelConfiguration;

namespace Linky.Entities.Configurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            HasMany(x => x.Links)
                .WithRequired(l => l.ApplicationUser)
                .HasForeignKey(l => l.ApplicationUserId);
        }
    }
}
