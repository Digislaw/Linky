using Linky.Entities.Identity;
using System;

namespace Linky.Entities.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string URL { get; set; }
        public int Clicks { get; set; } = 0;
        public DateTime CreatedAt { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
