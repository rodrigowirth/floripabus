using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace FloripaBus
{
	public interface IRouteRepository
	{
		Task<IList<Route>> FindRoutesByStopNameAsync(string stopName);
		Task<IList<RouteStop>> FindStopsByRouteIdAsync(int routeId);
		Task<IList<RouteDeparture>> FindDeparturesByRouteIdAsync(int routeId);
	}

	public class RouteRepository : IRouteRepository
	{		
		public async Task<IList<Route>> FindRoutesByStopNameAsync (string stopName)
		{
			string responseContent;
			using (var client = new HttpClient ()) 
			{
				var request = new HttpRequestMessage(HttpMethod.Post, "https://api.appglu.com/v1/queries/findRoutesByStopName/run");
				AddDefaultHeaders (request);

				var body = "{ \"params\": { \"stopName\": \"%" + stopName + "%\" } }";
				var content = new StringContent(body, Encoding.UTF8, "application/json");
				request.Content = content;

				var response = await client.SendAsync(request);
				responseContent = await response.Content.ReadAsStringAsync();
			}

			IList<Route> routes = new List<Route> ();
			RoutesReponse<RouteRow> data = JsonConvert.DeserializeObject<RoutesReponse<RouteRow>>(responseContent);
			foreach (var row in data.Rows)
			{
				routes.Add (new Route(row.Id, row.ShortName, row.LongName));
			}
				
			return routes;
		}

		public async Task<IList<RouteStop>> FindStopsByRouteIdAsync (int routeId)
		{
			string responseContent;
			using (var client = new HttpClient ()) 
			{
				var request = new HttpRequestMessage(HttpMethod.Post, "https://api.appglu.com/v1/queries/findStopsByRouteId/run");
				AddDefaultHeaders (request);

				var body = "{ \"params\": { \"routeId\": \"" + routeId + "\" } }";
				var content = new StringContent(body, Encoding.UTF8, "application/json");
				request.Content = content;

				var response = await client.SendAsync(request);
				responseContent = await response.Content.ReadAsStringAsync();
			}

			IList<RouteStop> routeStops = new List<RouteStop> ();
			RoutesReponse<StopRow> data = JsonConvert.DeserializeObject<RoutesReponse<StopRow>>(responseContent);
			foreach (var row in data.Rows)
			{
				routeStops.Add (new RouteStop(row.Name, row.Sequence));
			}

			return routeStops;
		}

		public async Task<IList<RouteDeparture>> FindDeparturesByRouteIdAsync (int routeId)
		{
			string responseContent;
			using (var client = new HttpClient ()) 
			{
				var request = new HttpRequestMessage(HttpMethod.Post, "https://api.appglu.com/v1/queries/findDeparturesByRouteId/run");
				AddDefaultHeaders (request);

				var body = "{ \"params\": { \"routeId\": \"" + routeId + "\" } }";
				var content = new StringContent(body, Encoding.UTF8, "application/json");
				request.Content = content;

				var response = await client.SendAsync(request);
				responseContent = await response.Content.ReadAsStringAsync();
			}

			IList<RouteDeparture> routeDepartures = new List<RouteDeparture> ();
			RoutesReponse<DepartureRow> data = JsonConvert.DeserializeObject<RoutesReponse<DepartureRow>>(responseContent);
			foreach (var row in data.Rows)
			{
				routeDepartures.Add (new RouteDeparture(row.Calendar, row.Time));
			}

			return routeDepartures;
		}

		private void AddDefaultHeaders(HttpRequestMessage request)
		{
			request.Headers.Add ("X-AppGlu-Environment", "staging");
			request.Headers.Add("Authorization", "Basic V0tENE43WU1BMXVpTThWOkR0ZFR0ek1MUWxBMGhrMkMxWWk1cEx5VklsQVE2OA==");
		}

		private class RoutesReponse<T>
		{
			public List<T> Rows {get;set;}
		}

		private class RouteRow
		{
			public int Id { get; set; }
			public string ShortName { get; set; }
			public string LongName { get; set; }
		}

		private class StopRow
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public int Sequence { get; set; }
		}

		private class DepartureRow
		{
			public int Id { get; set; }
			public string Calendar { get; set; }
			public string Time { get; set; }
		}
	}
}

