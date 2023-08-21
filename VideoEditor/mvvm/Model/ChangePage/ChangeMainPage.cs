using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Utilites;
using VideoEditor.mvvm.ViewModel;

namespace VideoEditor.mvvm.Model.ChangePage
{
	public static class ChangeMainPage // изменение главной ViewModel(MainWindowVM)
	{
		public static IChangePage MainViewModel;
		public static object Parameter { get; private set; }
		private static Pages _pages;

		static ChangeMainPage()
		{
			_pages = new Pages();
		}

		public static void ChangePage<T>(bool mode, object parameter = null) where T : INotifyPropertyChanged, new()
		{
			Parameter = parameter;
			MainViewModel.ChangePage(mode == true ? _pages.GetAndPut<T>() : new T());
		}
		public static void ChangePage(INotifyPropertyChanged model, bool mode, object parameter = null)
		{
			Parameter = parameter;
			MainViewModel.ChangePage(mode == true ? _pages.GetAndPut(model) : model);
		}
		public static INotifyPropertyChanged Pop() => _pages.Pop();
	}
}
