using System;
using System.Threading.Tasks;

namespace FloripaBus
{
	public interface INavigationService
	{
		Task NavigateToDetails(Route route);
	}

	public class NavigationService : INavigationService
	{
		public async Task NavigateToDetails (Route route)
		{
			await App.Current.MainPage.Navigation.PushAsync (new DetailsView (route));
		}
	}
}

