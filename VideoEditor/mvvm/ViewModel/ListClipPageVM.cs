using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;
using VideoEditor.mvvm.Utilites;
using VideoEditor.mvvm.Model.WorkingWithFile.Lifetime;
using VideoEditor.mvvm.Model.Settings;
using VideoEditor.mvvm.Model;
using VideoEditor.mvvm.Model.ChangePage;
using VideoEditor.mvvm.Model.WorkingWithFile;
using VideoEditor.mvvm.Model.WorkingForm;
using VideoEditor.mvvm.View;

namespace VideoEditor.mvvm.ViewModel
{
	public class ListClipPageVM : ViewModelBase
	{
		private ObservableCollection<VideoFile> _listVideos; // лист видео
		private VideoFile _selectedVideo; // выбранное видео из листа _listVideos
		private bool _isOpenProperty;
		private Dispatcher _dispatcher;

		private GeneralModelNotify<ClipTable> _generalModel;

		public ListClipPageVM()
		{
			_dispatcher = Dispatcher.CurrentDispatcher;
			FileLifetime fileLifetime = new FileLifetime();
			MainPack mainPack = MainPackSetting.GetPack().FillPack();
			_generalModel = new GeneralModelNotify<ClipTable>(ContextModelDataBase.GetContextModelDataBase().ElementAt("Test"));
			_generalModel.CreateTable();
			fileLifetime.AppendNotify(_generalModel);
			
			foreach (var item in _generalModel.GetAllTable())
				fileLifetime.AppendFile(new AdapterClipTableToVideoFile(item));

			fileLifetime.StartChecking(mainPack.CountLifetimeSetting.CountDays);

			ListVideos = fileLifetime.GetFiles() == null ? new ObservableCollection<VideoFile>() :
				new ObservableCollection<VideoFile>(new AdapterFile<VideoFile>().ListAdapter(fileLifetime.GetFiles()));
		}

		public ObservableCollection<VideoFile> ListVideos
		{
			get { return _listVideos; }
			set
			{
				_listVideos = value;
				Notify(nameof(ListVideos));
			}
		}

		public bool IsOpenProperty
		{
			get { return _isOpenProperty; }
			set
			{
				_isOpenProperty = value;
				Notify(nameof(IsOpenProperty));
			}
		}

		public VideoFile SelectedVideo // выбранное видео
		{
			get { return _selectedVideo; }
			set
			{
				_selectedVideo = value;
				Notify(nameof(SelectedVideo));
			}
		}

		public ButtonCommand DeleteBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					Stack<VideoFile> stackVideos = new Stack<VideoFile>();
					foreach (var item in ((System.Collections.IList)obj).Cast<VideoFile>())
					{
						item.DeleteFile();
						_generalModel.DeleteById(item.Id);
						stackVideos.Push(item);
					}
					foreach (var item in stackVideos)
					{
						_listVideos.Remove(item);
					}
				}, new Func<object, bool>((obj) =>
				{
					if (obj != null)
						if (((System.Collections.IList)obj).Count > 0)
							return true;

					return false;
				}));
			}
		}

		public ButtonCommand OpenFileBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					IsOpenProperty = false;
					ChangeMainPage.ChangePage<ShowVideoPageVM>(false, SelectedVideo);
					_selectedVideo = null;
				}, new Func<object, bool>((obj) =>
				{
					if (_selectedVideo != null)
						return true;
					return false;
				}));
			}
		}

		public ButtonCommand OpenPropertyBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					IsOpenProperty = false;
					try
					{
						UsingForms.PushAndOpen<VideoPropertyWindow>(_selectedVideo);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				});
			}
		}
	}
}
