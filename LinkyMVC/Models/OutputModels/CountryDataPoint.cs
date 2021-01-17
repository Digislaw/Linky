using System.Runtime.Serialization;

namespace LinkyMVC.Models.OutputModels
{
    [DataContract]
	public class CountryDataPoint
    {
        public CountryDataPoint() { }

		public CountryDataPoint(string label, double y)
		{
			Label = label;
			Y = y;
		}

		[DataMember(Name = "label")]
		public string Label = "";

		[DataMember(Name = "y")]
		public double? Y = null;
	}
}