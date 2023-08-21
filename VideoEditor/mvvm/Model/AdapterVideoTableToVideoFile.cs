using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table;
using VideoEditor.mvvm.Model.WorkingWithFile;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;

namespace VideoEditor.mvvm.Model
{
	public class AdapterVideoTableToVideoFile : VideoFile // адаптирует VideoTable в VideoFile
	{
		public AdapterVideoTableToVideoFile(VideoTable table)
		{
			Id = table.Id;
			Path = table.Path;
			Extension = table.Extension;
			NameFile = table.NameVideo;
			TimeCreate = table.TimeCreate;
			TimeExport = table.TimeExport;
		}
	}
}
