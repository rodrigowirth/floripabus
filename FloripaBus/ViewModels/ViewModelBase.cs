using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FloripaBus
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{		
		public event PropertyChangedEventHandler PropertyChanged;

		protected void Notify(string propertyName){
			if (this.PropertyChanged != null)
				this.PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

		protected void Notify<T>(Expression<Func<T>> e)
		{
			var memberExpression = e.Body as MemberExpression;

			if (memberExpression == null)
				throw new Exception ("Expression is not a Member Expression");

			var propertyName = memberExpression.Member.Name;

			this.Notify (propertyName);
		}

		protected async Task DisplayAlertAsync (string message)
		{
			await FloripaBus.App.Current.MainPage.DisplayAlert ("Floripa Bus", message, "ok");
		}
	}
}

