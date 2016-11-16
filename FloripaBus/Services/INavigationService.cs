using System;
using System.Threading.Tasks;

namespace FloripaBus
{
	public interface INavigationService
	{
		Task NavigateToDetailsAsync(Route route);
	}

	public class NavigationService : INavigationService
	{
		public async Task NavigateToDetailsAsync (Route route)
		{
			await App.Current.MainPage.Navigation.PushAsync (new DetailsView (route));
		}
	}
}

