using System.Runtime.Serialization;

namespace LinkyMVC.Models.OutputModels
{
	[DataContract]
	public class DailyDataPoint
	{
		public DailyDataPoint()
		{
			X = Y = null;
		}

		public DailyDataPoint(double x, double y)
		{
			X = x;
			Y = y;
		}

		[DataMember(Name = "x")]
		public double? X = null;

		[DataMember(Name = "y")]
		public double? Y = null;
	}
}