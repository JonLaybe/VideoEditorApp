using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.ChangePage;
using VideoEditor.mvvm.Utilites;

namespace VideoEditor.mvvm.ViewModel
{
	public class MainSettingsPageVM : ViewModelBase
	{
		private object _currentPage;
		private Pages _pages; // Хранит обьекты наследуемые от INotifyPropertyChanged

		public MainSettingsPageVM()
		{
			_pages = new Pages();
			CurrentPage = _pages.GetAndPut<AppearanceSettingsPageVM>();
		}

		public object CurrentPage
		{
			get { return _currentPage; }
			set
			{
				_currentPage = value;
				Notify(nameof(CurrentPage));
			}
		}
	}
}
