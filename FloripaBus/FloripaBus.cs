using System;

using Xamarin.Forms;

namespace FloripaBus
{
	public class App : Application
	{
		public App ()
		{
			DependencyService.Register<IRouteRepository, RouteRepository> ();

			// The root page of your application
			MainPage = new NavigationPage (new MainView ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

