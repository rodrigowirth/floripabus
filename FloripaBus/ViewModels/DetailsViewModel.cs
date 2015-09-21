using System;
using System.Collections.ObjectModel;

namespace FloripaBus
{
	public class DetailsViewModel : ViewModelBase
	{
		private readonly IRouteRepository _routeRepository;

		private Route _route;
		public Route Route {
			get {
				return _route;
			}
			set {
				_route = value;
				Notify ("Route");
			}
		}

		public string Title {
			get { return String.Format ("{0} - {1}", Route.ShortName, Route.LongName); }
		}

		private ObservableCollection<RouteStop> _routeStops;
		public ObservableCollection<RouteStop> RouteStops {
			get {
				return _routeStops;
			}
			set {
				_routeStops = value;
				Notify ("RouteStops");
			}
		}

		public DetailsViewModel (Route route, IRouteRepository routeRepository)
		{
			_routeRepository = routeRepository;

			this.Route = route;
			this.Load ();
		}

		public async void Load()
		{
			var stopsList = await _routeRepository.FindStopsByRouteIdAsync (this.Route.Id);
			this.RouteStops = new ObservableCollection<RouteStop> (stopsList);
		}
	}
}

