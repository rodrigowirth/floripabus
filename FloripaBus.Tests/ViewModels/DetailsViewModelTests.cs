using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace FloripaBus.Tests
{
	[TestFixture]
	public class DetailsViewModelTests
	{
		Route route = new Route (1, "198", "Brusque");
		Mock<IRouteRepository> routeRepositoryMock = new Mock<IRouteRepository> ();

		[Test]
		public void TheTitleShouldBeTheShortNamePlusTheLongName()
		{			
			var viewModel = new DetailsViewModel (route, routeRepositoryMock.Object);

			Assert.AreEqual ("198 - Brusque", viewModel.Title);				
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(10)]
		public void GivenAFewStopsThenShowTheseStops(int amount)
		{
			IList<RouteStop> routeStops = new List<RouteStop> ();

			for (int i = 0; i < amount; i++) {
				routeStops.Add(new RouteStop(string.Empty, i));
			}						

			routeRepositoryMock.Setup (x => x.FindStopsByRouteIdAsync (It.IsAny<int> ()))
				.ReturnsAsync (routeStops);

			var viewModel = new DetailsViewModel (route, routeRepositoryMock.Object);
			Assert.AreEqual (viewModel.RouteStops.Count, amount);
		}
	}
}

