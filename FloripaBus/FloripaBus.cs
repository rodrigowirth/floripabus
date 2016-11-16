using System;

using Xamarin.Forms;
using Ninject;
using Ninject.Modules;

namespace FloripaBus
{
	public class App : Application
	{
		public static StandardKernel Container { get; set; }

		public App ()
		{
			Ninja.LoadModule (new SharedModule ());

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

