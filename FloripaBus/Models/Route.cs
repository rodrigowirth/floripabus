using System;

namespace FloripaBus
{
	public class Route
	{
		public int Id { get; private set; }
		public string ShortName { get; private set; }
		public string LongName { get; private set; }

		public Route(int id, string shortName, string longName)
		{
			this.Id = id;
			this.ShortName = shortName;
			this.LongName = longName;
		}
	}	
}

