using System;

namespace FloripaBus
{
	public class DetailsViewModel : ViewModelBase
	{
		private int _routeId;
		public int RouteId {
			get {
				return _routeId;
			}
			set {
				_routeId = value;
				Notify ("RouteId");
			}
		}

		public DetailsViewModel ()
		{
			this.RouteId = 123;
		}
	}
}

