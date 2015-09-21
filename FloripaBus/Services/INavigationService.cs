using System;
using System.Threading.Tasks;

namespace FloripaBus
{
	public interface INavigationService
	{
		Task NavigateToDetails(int routeId);
	}

	public class NavigationService : INavigationService
	{
		public async Task NavigateToDetails (int routeId)
		{
			await App.Current.MainPage.Navigation.PushAsync (new DetailsView ());
		}
	}
}

