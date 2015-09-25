using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FloripaBus
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{		
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string propertyName){
			if (this.PropertyChanged != null)
				this.PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

		protected async Task DisplayAlertAsync (string message)
		{
			await FloripaBus.App.Current.MainPage.DisplayAlert ("Floripa Bus", message, "ok");
		}
	}
}

