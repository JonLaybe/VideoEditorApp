using Microsoft.WindowsAPICodePack.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VideoEditor.mvvm.Model.WorkingForm;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;
using VideoEditor.mvvm.Utilites;

namespace VideoEditor.mvvm.ViewModel
{
	public class VideoPropertyWindowVM : ViewModelBase
	{
		private Form _mainForm;
		private VideoFile _mainVidoeFile;

		public VideoPropertyWindowVM()
		{
			_mainForm = UsingForms.GetLastForm();
			_mainVidoeFile = (VideoFile)_mainForm.Parameter;
		}

		public VideoFile MainVideoFile
		{
			get { return _mainVidoeFile; }
			set
			{
				_mainVidoeFile = value;
				Notify(nameof(_mainVidoeFile));
			}
		}
	}
}
