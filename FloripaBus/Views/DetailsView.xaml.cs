using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Ninject.Parameters;

namespace FloripaBus
{
	public partial class DetailsView : TabbedPage
	{
		public DetailsView (Route route)
		{
			InitializeComponent ();
			this.BindingContext = Ninja.Get<DetailsViewModel> (new ConstructorArgument("route", route));
		}
	}
}

