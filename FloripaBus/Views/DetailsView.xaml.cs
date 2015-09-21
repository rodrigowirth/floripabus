using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FloripaBus
{
	public partial class DetailsView : TabbedPage
	{
		public DetailsView ()
		{
			InitializeComponent ();
			this.BindingContext = Ninja.Get<DetailsViewModel> ();
		}
	}
}

