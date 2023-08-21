using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VideoEditor.mvvm.Model;
using VideoEditor.mvvm.Model.ChangePage;
using VideoEditor.mvvm.Utilites;

namespace VideoEditor.mvvm.ViewModel
{
	public class MainPageVM : ViewModelBase
	{
		private object _currentPage;
		private Pages _pages; // Хранит обьекты наследуемые от INotifyPropertyChanged

		public MainPageVM()
		{
			_pages = new Pages();
			CurrentPage = _pages.GetAndPut<ListVideoPageVM>();
			//AppTheme.ChangeTheme(new Uri("Style\\Themes\\Dark.xaml", UriKind.Relative));
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

		public ButtonCommand MainPageCommand
		{
			get { return new ButtonCommand((object obj) => { CurrentPage = _pages.GetAndPut<ListVideoPageVM>(); }); }
		}
		public ButtonCommand ListClipCommand
		{
			get { return new ButtonCommand((object obj) => { CurrentPage = _pages.GetAndPut<ListClipPageVM>(); }); }
		}
		public ButtonCommand SettingCommnad
		{
			get { return new ButtonCommand((object obj) => { CurrentPage = _pages.GetAndPut<MainSettingsPageVM>(); }); }
		}
	}
}