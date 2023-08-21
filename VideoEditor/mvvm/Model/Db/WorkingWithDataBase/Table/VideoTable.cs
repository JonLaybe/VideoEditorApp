using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;

namespace VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table
{
	public class VideoTable : GeneralTable<VideoTable>
	{
		public VideoTable()
		{
			NameTable = "Video";
			Columns = new Dictionary<string, string>()
			{
				{ "Path", "TEXT" },
				{ "Extension", "TEXT" },
				{ "NameVideo", "TEXT"},
				{ "TimeCreate", "TEXT" },
				{ "TimeExport", "TEXT" },
			};
		}

		public int Id { get; set; }
		public string Path { get; set; } // путь к видео файлу
		public string Extension { get; set; } // Расширение видео файла
		public string NameVideo { get; set; } // Название видео файла
		public DateTime TimeCreate { get; set; } // Время создания видео файла
		public DateTime TimeExport { get; set; } // Время загрузки видео файла в приложение

		public override List<VideoTable> AdapterTable(DataTable dataTable)
		{
			List<VideoTable> listVideos = new List<VideoTable>();

			foreach (var item in dataTable.AsEnumerable())
			{
				listVideos.Add(new VideoTable() {
					Id = Convert.ToInt32(item[0]),
					Path = item[1].ToString(),
					Extension = item[2].ToString(),
					NameVideo = item[3].ToString(),
					TimeCreate = DateTime.Parse(item[4].ToString()),
					TimeExport = DateTime.Parse(item[5].ToString()),
				});
			}

			return listVideos;
		}
	}
}