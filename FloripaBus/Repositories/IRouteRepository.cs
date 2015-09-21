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
		void FindStopsByRouteId(int routeId);
		void FindDeparturesByRouteId(int routeId);
	}

	public class RouteRepository : IRouteRepository
	{
		public async Task<IList<Route>> FindRoutesByStopNameAsync (string stopName)
		{
			string responseContent;
			using (var client = new HttpClient ()) 
			{
				var request = new HttpRequestMessage(HttpMethod.Post, "https://api.appglu.com/v1/queries/findRoutesByStopName/run");
				request.Headers.Add ("X-AppGlu-Environment", "staging");
				request.Headers.Add("Authorization", "Basic V0tENE43WU1BMXVpTThWOkR0ZFR0ek1MUWxBMGhrMkMxWWk1cEx5VklsQVE2OA==");

				var body = "{ \"params\": { \"stopName\": \"%" + stopName + "%\" } }";
				var content = new StringContent(body, Encoding.UTF8, "application/json");
				request.Content = content;

				var response = await client.SendAsync(request);
				responseContent = await response.Content.ReadAsStringAsync();
			}

			IList<Route> routes = new List<Route> ();
			RoutesReponse data = JsonConvert.DeserializeObject<RoutesReponse>(responseContent);
			foreach (var row in data.Rows)
			{
				routes.Add (new Route(row.Id, row.ShortName, row.LongName));
			}
				
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

		private class RoutesReponse
		{
			public List<RouteRow> Rows {get;set;}
		}

		private class RouteRow
		{
			public int Id { get; set; }
			public string ShortName { get; set; }
			public string LongName { get; set; }
		}


	}
}

