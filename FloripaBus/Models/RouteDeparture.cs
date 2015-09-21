using System;

namespace FloripaBus
{
	public class RouteDeparture
	{
		public string Calendar { get; set; }
		public string Time { get; set; } 

		public RouteDeparture (string calendar, string time)
		{
			this.Calendar = calendar;
			this.Time = time;
		}
	}
}

