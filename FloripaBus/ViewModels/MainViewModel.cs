using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Windows.Input;

namespace FloripaBus
{
	public class MainViewModel : ViewModelBase
	{		
		private readonly IRouteRepository _routeRepository;
		private readonly INavigationService _navigationService;

		private ObservableCollection<Route> _routes;
		public ObservableCollection<Route> Routes {
			get {
				return _routes;
			}
			set {
				_routes = value;
				Notify (() => Routes);
			}
		}

		private string _searchText;
		public string SearchText {
			get {
				return _searchText;
			}
			set {
				_searchText = value;
				Notify (() => SearchText);
			}
		}

		private bool _isLoading;
		public bool IsLoading {
			get {
				return _isLoading;
			}
			set {
				_isLoading = value;
				Notify (() => IsLoading);
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

		private Route _selectedRoute = null;
		public Route SelectedRoute
		{
			get {
				return _selectedRoute;
			}
			set {
				if (value != null) {
					_navigationService.NavigateToDetailsAsync (value);
					Notify (() => SelectedRoute);
				}
			}
		}

		public MainViewModel(IRouteRepository routeRepository, INavigationService navigationService)
		{
			_routeRepository = routeRepository;
			_navigationService = navigationService;
			this.SearchCommand = new Command (this.SearchAsync);
			this.SearchAsync ();
		}			

		public async void SearchAsync()
		{
			this.IsLoading = true;

			try 
			{
				var routesList = await _routeRepository.FindRoutesByStopNameAsync (this.SearchText);
				this.Routes = new ObservableCollection<Route> (routesList);
			} 
			catch (Exception ex) 
			{
				this.DisplayAlertAsync (ex.Message);
			}

			this.IsLoading = false;
		}
	}
}

