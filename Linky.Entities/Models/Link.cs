using Linky.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Linky.Entities.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string URL { get; set; }
        public int Clicks { get; set; } = 0;
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime CreatedAt { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<CountryCounter> CountryCounters { get; set; }
        public virtual ICollection<DailyCounter> DailyCounters { get; set; }
    }
}
