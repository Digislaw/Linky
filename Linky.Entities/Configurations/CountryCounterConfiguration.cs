using Linky.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Linky.Entities.Configurations
{
    public class CountryCounterConfiguration : EntityTypeConfiguration<CountryCounter>
    {
        public CountryCounterConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CountryName).IsRequired().HasMaxLength(80);
            Property(x => x.Clicks).IsRequired();
        }
    }
}
