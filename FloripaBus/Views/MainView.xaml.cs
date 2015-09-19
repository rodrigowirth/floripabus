using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace FloripaBus
{
	public partial class MainView : ContentPage
	{
		public MainView ()
		{
			InitializeComponent ();
			this.BindingContext = Ninja.Get<MainViewModel> ();
		}
	}
}

