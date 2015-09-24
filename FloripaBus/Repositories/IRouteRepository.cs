using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Net;

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
		private const string FIND_ROUTES_URL = "https://api.appglu.com/v1/queries/findRoutesByStopName/run";
		private const string FIND_STOPS_URL = "https://api.appglu.com/v1/queries/findStopsByRouteId/run";
		private const string FIND_DEPARTURES_URL = "https://api.appglu.com/v1/queries/findDeparturesByRouteId/run";

		private readonly HttpClient _httpClient;

		public RouteRepository(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IList<Route>> FindRoutesByStopNameAsync (string stopName)
		{
			IList<Route> routes = new List<Route> ();

			using (var request = new HttpRequestMessage (HttpMethod.Post, FIND_ROUTES_URL)) {
			
				AddDefaultHeaders (request);

				var body = "{ \"params\": { \"stopName\": \"%" + stopName + "%\" } }";
				var content = new StringContent (body, Encoding.UTF8, "application/json");
				request.Content = content;

				var responseContent = await this.ExecuteRequestAsync (request);

				RoutesReponse<RouteRow> data = JsonConvert.DeserializeObject<RoutesReponse<RouteRow>> (responseContent);
				foreach (var row in data.Rows) {
					routes.Add (new Route (row.Id, row.ShortName, row.LongName));
				}
			}
				
			return routes;
		}

		public async Task<IList<RouteStop>> FindStopsByRouteIdAsync (int routeId)
		{
			IList<RouteStop> routeStops = new List<RouteStop> ();

			using (var request = new HttpRequestMessage (HttpMethod.Post, FIND_STOPS_URL)) {
				
				AddDefaultHeaders (request);

				var body = "{ \"params\": { \"routeId\": \"" + routeId + "\" } }";
				var content = new StringContent (body, Encoding.UTF8, "application/json");
				request.Content = content;

				var responseContent = await this.ExecuteRequestAsync (request);

				RoutesReponse<StopRow> data = JsonConvert.DeserializeObject<RoutesReponse<StopRow>> (responseContent);
				foreach (var row in data.Rows) {
					routeStops.Add (new RouteStop (row.Name, row.Sequence));
				}
			}

			return routeStops;
		}

		public async Task<IList<RouteDeparture>> FindDeparturesByRouteIdAsync (int routeId)
		{
			IList<RouteDeparture> routeDepartures = new List<RouteDeparture> ();

			using (var request = new HttpRequestMessage (HttpMethod.Post, FIND_DEPARTURES_URL)) {

				AddDefaultHeaders (request);

				var body = "{ \"params\": { \"routeId\": \"" + routeId + "\" } }";
				var content = new StringContent (body, Encoding.UTF8, "application/json");
				request.Content = content;

				var responseContent = await this.ExecuteRequestAsync (request);

				RoutesReponse<DepartureRow> data = JsonConvert.DeserializeObject<RoutesReponse<DepartureRow>> (responseContent);
				foreach (var row in data.Rows) {
					routeDepartures.Add (new RouteDeparture (row.Calendar, row.Time));
				}
			}

			return routeDepartures;
		}

		private void AddDefaultHeaders(HttpRequestMessage request)
		{			
			var authentication = string.Format("{0}:{1}", "WKD4N7YMA1uiM8V", "DtdTtzMLQlA0hk2C1Yi5pLyVIlAQ68");
			var encodedAuthentication = Convert.ToBase64String(Encoding.UTF8.GetBytes(authentication));
			request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", encodedAuthentication);

			request.Headers.Add ("X-AppGlu-Environment", "staging");
		}

		private async Task<string> ExecuteRequestAsync(HttpRequestMessage request)
		{
			var response = await _httpClient.SendAsync(request);

			if (response.StatusCode != HttpStatusCode.OK)
				throw new Exception ("Fail to contact the server");

			var responseContent = await response.Content.ReadAsStringAsync();
			return responseContent;
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

