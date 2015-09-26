using System;
using NUnit.Framework;
using System.Net.Http;
using System.Net;
using System.Text;

namespace FloripaBus.Tests
{
	[TestFixture]
	public class RoutesRepositoryTests
	{
		private const string FIND_ROUTES_URL = "https://api.appglu.com/v1/queries/findRoutesByStopName/run";
		private const string FIND_STOPS_URL = "https://api.appglu.com/v1/queries/findStopsByRouteId/run";
		private const string FIND_DEPARTURES_URL = "https://api.appglu.com/v1/queries/findDeparturesByRouteId/run";

		private MockResponseHandler _responseHandler;

		[SetUp]
		public void SetUp()
		{
			_responseHandler = new MockResponseHandler();
		}

		[Test]
		public void WhenGetsANonOkResponseShouldThrowsAnException()
		{
			_responseHandler.AddResponse(FIND_ROUTES_URL, new HttpResponseMessage(HttpStatusCode.BadRequest));
			_responseHandler.AddResponse(FIND_STOPS_URL, new HttpResponseMessage(HttpStatusCode.BadRequest));
			_responseHandler.AddResponse(FIND_DEPARTURES_URL, new HttpResponseMessage(HttpStatusCode.BadRequest));
			var client = new HttpClient (_responseHandler);

			var repository = new RouteRepository (client);

			Assert.Throws<Exception> (async () => await repository.FindRoutesByStopNameAsync (string.Empty));
			Assert.Throws<Exception> (async () => await repository.FindStopsByRouteIdAsync (0));
			Assert.Throws<Exception> (async () => await repository.FindDeparturesByRouteIdAsync (0));
		}

		[Test]
		public async void GivenOneRouteFromServiceThenReturnsThisRouteWithItsData()
		{
			var responseContent = "{ \"rows\": [ { \"id\": 32, \"shortName\": \"133\", \"longName\": \"AGRONÔMICA VIA MAURO RAMOS\", \"lastModifiedDate\": \"2012-07-23T03:00:00+0000\", \"agencyId\": 9 } ], \"rowsAffected\": 0 }";
			var response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new StringContent (responseContent);
			_responseHandler.AddResponse(FIND_ROUTES_URL, response);
			var client = new HttpClient (_responseHandler);

			var repository = new RouteRepository (client);
			var routes = await repository.FindRoutesByStopNameAsync (string.Empty);

			Assert.AreEqual (1, routes.Count);
			Assert.AreEqual (32, routes [0].Id);
			Assert.AreEqual ("133", routes [0].ShortName);
			Assert.AreEqual ("AGRONÔMICA VIA MAURO RAMOS", routes [0].LongName);
		}

		[Test]
		public async void GivenTwoStopFromServiceThenReturnsTheseStopsWithTheirData()
		{
			var responseContent = new StringBuilder ()
				.Append ("{ \"rows\": [")
				.Append ("{ \"id\": 1, \"name\": \"TICEN\", \"sequence\": 1, \"route_id\": 35 },")
				.Append ("{ \"id\": 2, \"name\": \"RUA ANTÔNIO PEREIRA OLIVEIRA NETO\", \"sequence\": 2, \"route_id\": 35 }")
				.Append ("], \"rowsAffected\": 0 }")
				.ToString ();
					
			var response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new StringContent (responseContent);
			_responseHandler.AddResponse(FIND_STOPS_URL, response);
			var client = new HttpClient (_responseHandler);

			var repository = new RouteRepository (client);
			var stops = await repository.FindStopsByRouteIdAsync (0);

			Assert.AreEqual (2, stops.Count);
			Assert.AreEqual ("TICEN", stops [0].Name);
			Assert.AreEqual (1, stops [0].Sequence);
			Assert.AreEqual ("RUA ANTÔNIO PEREIRA OLIVEIRA NETO", stops [1].Name);
			Assert.AreEqual (2, stops [1].Sequence);
		}

		[Test]
		public async void GivenThreeDeparturesThenReturnsTheseDeparturesWithTheirData()
		{
			var responseContent = new StringBuilder ()
				.Append ("{ \"rows\": [")
				.Append ("{ \"id\": 76, \"calendar\": \"WEEKDAY\", \"time\": \"00:07\" },")
				.Append ("{ \"id\": 114, \"calendar\": \"SATURDAY\", \"time\": \"23:35\" },")
				.Append ("{ \"id\": 133, \"calendar\": \"SUNDAY\", \"time\": \"23:29\" }")
				.Append ("], \"rowsAffected\": 0 }")
				.ToString ();

			var response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new StringContent (responseContent);
			_responseHandler.AddResponse(FIND_DEPARTURES_URL, response);
			var client = new HttpClient (_responseHandler);

			var repository = new RouteRepository (client);
			var departures = await repository.FindDeparturesByRouteIdAsync (0);

			Assert.AreEqual (3, departures.Count);
			Assert.AreEqual ("WEEKDAY", departures [0].Calendar);
			Assert.AreEqual ("00:07", departures [0].Time);
			Assert.AreEqual ("SATURDAY", departures [1].Calendar);
			Assert.AreEqual ("23:35", departures [1].Time);
			Assert.AreEqual ("SUNDAY", departures [2].Calendar);
			Assert.AreEqual ("23:29", departures [2].Time);
		}
	}


}

