using System;

namespace Linky.Entities.Models
{
    public class DailyCounter
    {
        public int Id { get; set; }
        public DateTime Day { get; set; } = DateTime.Now.Date;
        public int Clicks { get; set; } = 0;

        public int LinkId { get; set; }
        public virtual Link Link { get; set; }
    }
}
