using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using VideoEditor.mvvm.Model.ChangePage;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table;
using VideoEditor.mvvm.Model.FileProcessing;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;
using VideoEditor.mvvm.Utilites;

namespace VideoEditor.mvvm.ViewModel
{
	public class VideoEditoirVM : ViewModelBase
	{
		private VideoFile _mainVideo;
		private VideoProcessing _videoProcessing;
		private Dispatcher _dispatcher;
		private string _nameSaveFile; // имя сохраняемого файла
		private Visibility _visibilitySaveForm; // видемость формы сохранения
		private bool _isEnabledMainField; // Активна ли главное поле
		private bool _isBlockButtonSaveForm; // Бликированны ли кнопуи в сохраняемой форме
		private double _timeMin;
		private double _timeMax;
		private ObservableCollection<BitmapImage> _listBitmapImages;
		public double Milliseconds { get; set; }
		private int _startFrame;
		private int _endFrame;

		private GeneralModelNotify<ClipTable> _generalModel;

		public VideoEditoirVM()
		{
			_dispatcher = Dispatcher.CurrentDispatcher;
			MainVideo = (VideoFile)ChangeMainPage.Parameter;
			_nameSaveFile = "";
			_isEnabledMainField = true;
			_isBlockButtonSaveForm = false;
			_visibilitySaveForm = Visibility.Hidden;
			_videoProcessing = new VideoProcessing(_mainVideo);

			_generalModel = new GeneralModelNotify<ClipTable>(ContextModelDataBase.GetContextModelDataBase().ElementAt("DatabaseVideo"));
			_generalModel.CreateTable();

			StartFrame = 0;
			EndFrame = CountFrame;

			ListBitmapImages = new ObservableCollection<BitmapImage>();

			//Task<List<BitmapImage>> taskListImagse = _videoProcessing.GetFrameVideoAsync();

			/*Task.Run(async () =>
			{
				await taskListImagse;

				_dispatcher.Invoke(() =>
				{
					foreach (var item in taskListImagse.Result)
					{
						ListBitmapImages.Add(item);
					}

					MessageBox.Show("Frames " + (ListBitmapImages.Count()/3).ToString());
				});
			});*/
		}

		public ObservableCollection<BitmapImage> ListBitmapImages
		{
			get { return _listBitmapImages; }
			set
			{
				_listBitmapImages = value;
				Notify(nameof(ListBitmapImages));
			}
		}

		private void ClearSaveForm() // Отчищает форму сохранения
		{
			VisibilitySaveForm = Visibility.Hidden;
			IsEnabledMainField = true;
			NameSaveFile = string.Empty;
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

		public int CountFrame
		{
			get { return _videoProcessing.CountFrame; }
		}

		public double TimeMin
		{
			get { return _timeMin; }
			set
			{
				_timeMin = value;
				Notify(nameof(TimeMin));
			}
		}

		public double TimeMax
		{
			get { return _timeMax; }
			set
			{
				_timeMax = value;
				Notify(nameof(TimeMax));
			}
		}

		public int StartFrame
		{
			get { return _startFrame; }
			set
			{
				_startFrame = value;
				TimeMin = value * _videoProcessing.DelayBetweenFrames; // переводит текущий кадр в время позиции этого кадра
				Notify(nameof(StartFrame));
			}
		}

		public int EndFrame
		{
			get { return _endFrame; }
			set
			{
				_endFrame = value;
				TimeMax = (value * _videoProcessing.DelayBetweenFrames) - _videoProcessing.DelayBetweenFrames; // переводит текущий кадр в время позиции этого кадра
				Notify(nameof(EndFrame));
			}
		}

		public Visibility VisibilitySaveForm
		{
			get { return _visibilitySaveForm; }
			set
			{
				_visibilitySaveForm = value;
				Notify(nameof(VisibilitySaveForm));
			}
		}

		public bool IsEnabledMainField
		{
			get { return _isEnabledMainField; }
			set
			{
				_isEnabledMainField = value;
				Notify(nameof(IsEnabledMainField));
			}
		}

		public ButtonCommand ExitBtn
		{
			get
			{
				return new ButtonCommand((object obj) => {
					ChangeMainPage.ChangePage(ChangeMainPage.Pop(), true);
				});
			}
		}

		public string NameSaveFile
		{
			get { return _nameSaveFile; }
			set
			{
				_nameSaveFile = value;
				Notify(nameof(NameSaveFile));
			}
		}

		public ButtonCommand OpenSaveFormBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					IsEnabledMainField = false;
					VisibilitySaveForm = Visibility.Visible;
				});
			}
		}

		public ButtonCommand CloseSaveFormBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					ClearSaveForm();
				}, new Func<object, bool>((object obj) =>
				{
					return !_isBlockButtonSaveForm;
				}));
			}
		}

		public ButtonCommand SaveClipBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					Task.Run(() =>
					{
						try
						{
							_isBlockButtonSaveForm = true;
							//TODO путь брать из settings
							VideoFile videoFile = _videoProcessing.VideoCropping(new DirectoryInfo(
								$"Clips\\Clp{DateTime.Now.ToString().Replace(" ", "").Replace(":", "")}.mp4").FullName,
								StartFrame, EndFrame);

							_generalModel.Insert(new string[7]
							{
									videoFile.Path,
									videoFile.Extension,
									videoFile.NameFile,
									_nameSaveFile,
									"",
									DateTime.Now.ToString(),
									DateTime.Now.ToString(),
							});

							ClearSaveForm();
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
					});
				}, new Func<object, bool>((object obj) =>
				{
					if (_isBlockButtonSaveForm || _nameSaveFile.Length == 0)
						return false;
					return true;
				}));
			}
		}

		~VideoEditoirVM()
		{
			try
			{
				_dispatcher.Invoke(() =>
				{
					_listBitmapImages.Clear();
				});
			}catch(Exception ex)
			{

			}
		}
	}
}