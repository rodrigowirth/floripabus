using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Windows.Input;

namespace FloripaBus
{
	public class MainViewModel : ViewModelBase
	{
		private readonly IRouteRepository _routeRepository;

		private ObservableCollection<Route> _routes;
		public ObservableCollection<Route> Routes {
			get {
				return _routes;
			}
			set {
				_routes = value;
				Notify ("Routes");
			}
		}

		private string _searchText;
		public string SearchText {
			get {
				return _searchText;
			}
			set {
				_searchText = value;
				Notify ("SearchText");
			}
		}

		private bool _isLoading;
		public bool IsLoading {
			get {
				return _isLoading;
			}
			set {
				_isLoading = value;
				Notify ("IsLoading");
			}
		}

		private ICommand _searchCommand;
		public ICommand SearchCommand {
			get {
				return _searchCommand;
			}
			set {
				_searchCommand = value;
			}
		}

		public MainViewModel(IRouteRepository routeRepository)
		{
			_routeRepository = routeRepository;
			this.SearchCommand = new Command (this.Search);
			this.Load ();
		}

		public async void Load()
		{
			this.IsLoading = true;

			var routesList = await _routeRepository.FindRoutesByStopNameAsync (string.Empty);
			this.Routes = new ObservableCollection<Route> (routesList);

			this.IsLoading = false;
		}

		public async void Search()
		{
			this.IsLoading = true;

			var routesList = await _routeRepository.FindRoutesByStopNameAsync (this.SearchText);
			this.Routes = new ObservableCollection<Route> (routesList);

			this.IsLoading = false;
		}
	}
}

