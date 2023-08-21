using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VideoEditor.mvvm.Model.WorkingWithFile.Interface;

namespace VideoEditor.mvvm.Model.WorkingWithFile.Files.Video
{
	public class VideoFile : GeneralFile, IFunctionImportFile
	{
		public TimeSpan StartTime; // На какой минуте видео воспроизводится

		public TimeSpan DurationTime { get; set; } // Длительность видео файла

		public BitmapImage PreviewVideo { get; set; } // Превью видео

		public GeneralFile Import(string path) => new AdapterGeneralFile(new FileInfo(path)); // импортирует файл в проект (path - путь к файлу)

		public VideoFile() { }
		public VideoFile(string path) : base(path) { }
	}
}
