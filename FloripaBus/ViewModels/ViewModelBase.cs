using System;
using System.ComponentModel;

namespace FloripaBus
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		//protected readonly Services.INavigationService _navigationService;
		//protected readonly Services.IMessageService _messageService;

		public ViewModelBase(){
			//this._navigationService = DependencyService.Get<Services.INavigationService> ();
			//this._messageService = DependencyService.Get<Services.IMessageService> ();
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		protected void Notify(string propertyName){
			if (this.PropertyChanged != null)
				this.PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}

