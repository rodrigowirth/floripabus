using System;
using Ninject.Modules;
using System.Net.Http;

namespace FloripaBus
{
	public class SharedModule : NinjectModule
	{
		public override void Load() 
		{			
			this.Bind<HttpClient> ().ToSelf ();

			this.Bind<INavigationService> ().To<NavigationService> ();

			this.Bind<IRouteRepository>().To<RouteRepository>();

			this.Bind<MainViewModel> ().ToSelf ();
			this.Bind<DetailsViewModel> ().ToSelf ();
		}
	}
}

