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
		Mock<IRouteRepository> routeRepositoryMock = new Mock<IRouteRepository> ();

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(10)]
		public void GivenAFewRoutesThenShowTheseRoutes(int amount)
		{
			IList<Route> routes = new List<Route> ();

			for (int i = 0; i < amount; i++) {
				routes.Add(new Route(i, String.Empty, String.Empty));
			}						

			routeRepositoryMock.Setup (x => x.FindRoutesByStopNameAsync (It.IsAny<String> ()))
				.ReturnsAsync (routes);

			var viewModel = new MainViewModel (routeRepositoryMock.Object);
			Assert.AreEqual (viewModel.Routes.Count, amount);
		}

		[Test]
		public void GivenARouteThenShowItsData()
		{
			IList<Route> routes = new List<Route> ();
			routes.Add(new Route(1, "110", "Coral"));

			var routeRepositoryMock = new Mock<IRouteRepository> ();
			routeRepositoryMock.Setup (x => x.FindRoutesByStopNameAsync (It.IsAny<String> ()))
				.ReturnsAsync (routes);

			var viewModel = new MainViewModel (routeRepositoryMock.Object);
			Assert.AreEqual (viewModel.Routes[0].Id, 1);
			Assert.AreEqual (viewModel.Routes[0].ShortName, "110");
			Assert.AreEqual (viewModel.Routes[0].LongName, "Coral");
		}

		[Test]
		public void WhenExecutingSearchCommandShouldGetRoutesBasedOnTheStopNamesToSearch()
		{
			var viewModel = new MainViewModel (routeRepositoryMock.Object);
			viewModel.SearchText = "Castelo Branco";

			viewModel.SearchCommand.Execute (null);

			routeRepositoryMock.Verify(x => x.FindRoutesByStopNameAsync("Castelo Branco"), Times.Once);
		}
	}
}

