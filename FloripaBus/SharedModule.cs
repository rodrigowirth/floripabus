using System;
using Ninject.Modules;

namespace FloripaBus
{
	public class SharedModule : NinjectModule
	{
		public override void Load() 
		{
			this.Bind<INavigationService> ().To<NavigationService> ();

			this.Bind<IRouteRepository>().To<RouteRepository>();

			this.Bind<MainViewModel> ().ToSelf ();
			this.Bind<DetailsViewModel> ().ToSelf ();
		}
	}
}

