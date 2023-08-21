using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VideoEditor.mvvm.Model.ChangePage;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table;
using VideoEditor.mvvm.Model.FileProcessing;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;
using VideoEditor.mvvm.Utilites;

namespace VideoEditor.mvvm.ViewModel
{
	public class ShowVideoPageVM : ViewModelBase
	{
		private bool _isPlay;
		private VideoFile _mainVideo;
		private TimeSpan _currentTime;
		private double _valueTimePosution;

		public ShowVideoPageVM()
		{
			_isPlay = false;
			MainVideo = (VideoFile)ChangeMainPage.Parameter;
			ValueTimePosution = 0;
			Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
		}

		public VideoFile MainVideo
		{
			get { return _mainVideo; }
			set
			{
				_mainVideo = value;
				Notify(nameof(MainVideo));
			}
		}

		public bool IsPlay
		{
			get { return _isPlay; }
			set
			{
				_isPlay = value;
				Notify(nameof(IsPlay));
			}
		}

		public TimeSpan DurationTime
		{
			get { return _mainVideo.DurationTime; }
			set
			{
				_mainVideo.DurationTime = value;
				Notify(nameof(DurationTime));
			}
		}

		public TimeSpan CurrentTime
		{
			get { return _currentTime; }
			set
			{
				_currentTime = value;
				Notify(nameof(CurrentTime));
			}
		}

		public double ValueTimePosution
		{
			get { return _valueTimePosution; }
			set
			{
				_valueTimePosution = value;
				Notify(nameof(ValueTimePosution));
			}
		}

		public ButtonCommand ExitBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					IsPlay = false;
					ChangeMainPage.ChangePage<MainPageVM>(true);
				});
			}
		}

		public ButtonCommand OpenVideoEditorPage
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					ChangeMainPage.ChangePage<VideoEditoirVM>(false, _mainVideo);
				});
			}
		}
	}
}
