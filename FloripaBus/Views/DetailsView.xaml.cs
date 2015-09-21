using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FloripaBus
{
	public partial class DetailsView : ContentPage
	{
		public DetailsView ()
		{
			InitializeComponent ();
			this.BindingContext = Ninja.Get<DetailsViewModel> ();
		}
	}
}

