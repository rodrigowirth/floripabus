using System;
using NUnit.Framework;
using System.Collections.Generic;
using Xamarin.Forms;
using Moq;

namespace FloripaBus.Tests
{
	[TestFixture]
	public class MainViewModelTests
	{
		[Test]
		public void GivenThreeRoutesThenShowTheseThreeRoutes()
		{
			IList<Route> routes = new List<Route> ();
			routes.Add(new Route(1, "110", "Coral"));
			routes.Add(new Route(2, "223", "Centro - Direto"));
			routes.Add(new Route(3, "171", "Brusque"));

			var routeRepositoryMock = new Mock<IRouteRepository> ();
			routeRepositoryMock.Setup (x => x.FindRoutesByStopNameAsync (It.IsAny<String> ()))
				.ReturnsAsync (routes);

			var viewModel = new MainViewModel (routeRepositoryMock.Object);
			Assert.AreEqual (viewModel.Routes.Count, 3);
		}
	}
}

