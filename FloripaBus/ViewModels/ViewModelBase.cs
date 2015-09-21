using System;
using System.ComponentModel;

namespace FloripaBus
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{		
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		protected void Notify(string propertyName){
			if (this.PropertyChanged != null)
				this.PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}

