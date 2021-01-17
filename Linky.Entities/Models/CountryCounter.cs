namespace Linky.Entities.Models
{
    public class CountryCounter
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public int Clicks { get; set; } = 0;

        public int LinkId { get; set; }
        public virtual Link Link { get; set; }
    }
}
