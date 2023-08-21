using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;

namespace VideoEditor.mvvm.Model
{
	public class AdapterClipTableToVideoFile : VideoFile
	{
		public AdapterClipTableToVideoFile(ClipTable table)
		{
			Id = table.Id;
			Path = table.Path;
			Extension = table.Extension;
			NameFile = table.TitleVideo;
			TimeCreate = table.TimeCreate;
			TimeExport = table.TimeExport;
		}
	}
}
