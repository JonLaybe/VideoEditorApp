using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VideoEditor.mvvm.Model;
using VideoEditor.mvvm.Model.ChangePage;
using VideoEditor.mvvm.Model.Db;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table;
using VideoEditor.mvvm.Model.Settings;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;
using VideoEditor.mvvm.Model.WorkingWithFile.Lifetime;
using VideoEditor.mvvm.Utilites;

namespace VideoEditor.mvvm.ViewModel
{
	public class MainWindowVM : ViewModelBase, IChangePage
	{
		private object _currentPage;

		public MainWindowVM()
		{
			ChangeMainPage.MainViewModel = this;
			MainPackSetting mainPackSetting = MainPackSetting.GetPack();
			ContextModelDataBase contextModelDataBase = ContextModelDataBase.GetContextModelDataBase();
			GeneralModelNotify<VideoTable> _generalModel;
			FileLifetime fileLifetime = new FileLifetime();

			MainPack mainPack = mainPackSetting.FillPack();

			contextModelDataBase.CreatingConnecting("DatabaseVideo");

			_generalModel = new GeneralModelNotify<VideoTable>(contextModelDataBase.ElementAt("DatabaseVideo"));
			fileLifetime.AppendNotify(_generalModel);
			_generalModel.CreateTable();

			foreach (var item in _generalModel.GetAllTable())
				fileLifetime.AppendFile(new AdapterVideoTableToVideoFile(item));

			fileLifetime.StartChecking(mainPack.CountLifetimeSetting.CountDays);
			ChangeMainPage.ChangePage<MainPageVM>(true, fileLifetime.GetFiles());
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

		public void ChangePage(INotifyPropertyChanged page)
		{
			CurrentPage = page;
		}
	}
}
