using Linky.Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Linky.Entities.Configurations
{
    public class DailyCounterConfiguration : EntityTypeConfiguration<DailyCounter>
    {
        public DailyCounterConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Day).IsRequired().HasColumnType("date");
            Property(x => x.Clicks).IsRequired();
        }
    }
}
