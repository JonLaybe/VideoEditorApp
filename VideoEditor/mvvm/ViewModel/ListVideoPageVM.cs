using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.WindowsAPICodePack.Shell;
using VideoEditor.mvvm.Model.ChangePage;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table;
using VideoEditor.mvvm.Model.WorkingWithFile;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;
using VideoEditor.mvvm.Utilites;
using VideoEditor.mvvm.Model;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Windows.Media.Animation;
using System.ComponentModel;
using VideoEditor.mvvm.View;
using VideoEditor.mvvm.Model.WorkingForm;

namespace VideoEditor.mvvm.ViewModel
{
	public class ListVideoPageVM : ViewModelBase
	{
		private ObservableCollection<VideoFile> _listVideos; // лист видео
		private VideoFile _selectedVideo; // выбранное видео из листа _listVideos
		private bool _isOpenProperty;
		private Dispatcher _dispatcher;

		private GeneralModelNotify<VideoTable> _generalModel;

		public ListVideoPageVM()
		{
			//MessageBox.Show(MainPackSetting.GetPack().Pack.DefaultDirectory.Dirs.Count().ToString());
			_dispatcher = Dispatcher.CurrentDispatcher;
			_generalModel = new GeneralModelNotify<VideoTable>(ContextModelDataBase.GetContextModelDataBase().ElementAt("DatabaseVideo"));
			//IsOpenProperty = true;

			ListVideos = ChangeMainPage.Parameter == null ? new ObservableCollection<VideoFile>() :
				new ObservableCollection<VideoFile>(new AdapterFile<VideoFile>().ListAdapter((List<GeneralFile>)ChangeMainPage.Parameter));

			foreach (var item in ListVideos)
				item.PreviewVideo = GetPreview(item.Path);
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

		public ButtonCommand OpenVideoBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					OpenFileDialog openFileDialog = new OpenFileDialog();
					openFileDialog.Filter = "(*.mp4)|*.mp4|(*wmv)|*.wmv";

					if (openFileDialog.ShowDialog() == true)
					{
						try
						{
							VideoFile videoFile = new VideoFile();

							Task.Run(() =>
							{
								videoFile.Copy(openFileDialog.FileName, new DirectoryInfo("Video").FullName); // TODO "Video" путь из settings

								_generalModel.InsertAsync(new string[5]
								{
									videoFile.Path,
									videoFile.Extension,
									videoFile.NameFile,
									new FileInfo(videoFile.Path).CreationTime.ToString(),
									videoFile.TimeExport.ToString(),
								});

								_dispatcher.Invoke(() =>
								{
									videoFile.PreviewVideo = GetPreview(videoFile.Path);
									ListVideos.Add(videoFile);
								});
							});
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
					}
				});
			}
		}

		public BitmapImage GetPreview(string pathVideo)
		{
			ShellFile shellFile = ShellFile.FromFilePath(pathVideo);
			return ConvertBitmapImage.Convert(shellFile.Thumbnail.Bitmap);
		}

		public ButtonCommand DeleteBtn
		{
			get
			{
				return new ButtonCommand((object obj) =>
				{
					IsOpenProperty = false;
					_selectedVideo.DeleteFile();
					_generalModel.DeleteById(_selectedVideo.Id);
					_listVideos.Remove(_selectedVideo);
					/*Stack<VideoFile> stackVideos = new Stack<VideoFile>();
					foreach (var item in ((System.Collections.IList)obj).Cast<VideoFile>())
					{
						item.DeleteFile();
						_generalModel.DeleteById(item.Id);
						stackVideos.Push(item);
					}
					foreach (var item in stackVideos)
					{
						_listVideos.Remove(item);
					}*/
				}, new Func<object, bool>((obj) => {
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
