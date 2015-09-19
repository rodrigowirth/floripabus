using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FloripaBus
{
	public interface IRouteRepository
	{
		Task<IList<Route>> FindRoutesByStopNameAsync(string stopName);
		void FindStopsByRouteId(int routeId);
		void FindDeparturesByRouteId(int routeId);
	}

	public class RouteRepository : IRouteRepository
	{
		public async Task<IList<Route>> FindRoutesByStopNameAsync (string stopName)
		{
			IList<Route> routes = new List<Route> ();
			routes.Add(new Route(1, "110", "Coral"));
			routes.Add(new Route(2, "223", "Centro - Direto"));
			routes.Add(new Route(3, "171", "Brusque"));

			return routes;
		}

		public void FindStopsByRouteId (int routeId)
		{
			throw new NotImplementedException ();
		}

		public void FindDeparturesByRouteId (int routeId)
		{
			throw new NotImplementedException ();
		}
	}
}

