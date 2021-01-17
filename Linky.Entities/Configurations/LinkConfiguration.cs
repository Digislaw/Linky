using Linky.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Linky.Entities.Configurations
{
    public class LinkConfiguration : EntityTypeConfiguration<Link>
    {
        public LinkConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Label).IsRequired().HasMaxLength(50);
            Property(x => x.URL).IsRequired().HasMaxLength(256);
            Property(x => x.Clicks).IsRequired();
            Property(x => x.CreatedAt).IsRequired();

            HasMany(x => x.CountryCounters)
                .WithRequired(c => c.Link)
                .HasForeignKey(c => c.LinkId);
        }
    }
}
