using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FloripaBus
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IRouteRepository _routeRepository;

		ObservableCollection<Route> routes;
		public ObservableCollection<Route> Routes {
			get {
				return routes;
			}
			set {
				routes = value;
				Notify ("Routes");
			}
		}

		public MainViewModel()
		{
			_routeRepository = DependencyService.Get<IRouteRepository> ();
			this.Load ();
		}

		public async void Load()
		{
			var routesList = await _routeRepository.FindRoutesByStopNameAsync (string.Empty);
			this.Routes = new ObservableCollection<Route> (routesList);
		}
	}
}

