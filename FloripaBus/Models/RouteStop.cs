using System;

namespace FloripaBus
{
	public class RouteStop
	{
		public string Name { get; private set; }
		public int Sequence { get; private set;}

		public RouteStop(string name, int sequence)
		{
			this.Name = name;
			this.Sequence = sequence;
		}
	}
}

