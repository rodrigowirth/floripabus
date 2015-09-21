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

		private ObservableCollection<RouteDeparture> _weekDayDepartures;
		public ObservableCollection<RouteDeparture> WeekDayDepartures {
			get {
				return _weekDayDepartures;
			}
			set {
				_weekDayDepartures = value;
				Notify ("WeekDayDepartures");
			}
		}

		private ObservableCollection<RouteDeparture> _saturdayDepartures;
		public ObservableCollection<RouteDeparture> SaturdayDepartures {
			get {
				return _saturdayDepartures;
			}
			set {
				_saturdayDepartures = value;
				Notify ("SaturdayDepartures");
			}
		}

		private ObservableCollection<RouteDeparture> _sundayDepartures;
		public ObservableCollection<RouteDeparture> SundayDepartures {
			get {
				return _sundayDepartures;
			}
			set {
				_sundayDepartures = value;
				Notify ("SundayDepartures");
			}
		}

		private bool _isLoading;
		public bool IsLoading {
			get {
				return _isLoading;
			}
			set {
				_isLoading = value;
				this.ShowData = !_isLoading;
				Notify ("IsLoading");
			}
		}

		private bool _showData;
		public bool ShowData {
			get {
				return _showData;
			}
			set {
				_showData = value;
				Notify ("ShowData");
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
			this.IsLoading = true;

			var stopsList = await _routeRepository.FindStopsByRouteIdAsync (this.Route.Id);
			this.RouteStops = new ObservableCollection<RouteStop> (stopsList);

			this.WeekDayDepartures = new ObservableCollection<RouteDeparture> ();
			this.SaturdayDepartures = new ObservableCollection<RouteDeparture> ();
			this.SundayDepartures = new ObservableCollection<RouteDeparture> ();

			var departuresList = await _routeRepository.FindDeparturesByRouteIdAsync (this.Route.Id);
			foreach (var departure in departuresList) {
				switch (departure.Calendar) {
				case "WEEKDAY":
					this.WeekDayDepartures.Add (departure);
					break;
				case "SUNDAY":
					this.SundayDepartures.Add (departure);
					break;
				case "SATURDAY":
					this.SaturdayDepartures.Add (departure);
					break;
				default:
					break;
				}
			}

			this.IsLoading = false;
		}
	}
}

