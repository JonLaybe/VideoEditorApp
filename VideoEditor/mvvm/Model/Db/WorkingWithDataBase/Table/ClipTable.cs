using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Table
{
	public class ClipTable : GeneralTable<ClipTable>
	{
		public ClipTable()
		{
			NameTable = "Clip";
			Columns = new Dictionary<string, string>()
			{
				{ "Path", "TEXT" },
				{ "Extension", "TEXT" },
				{ "NameVideo", "TEXT"},
				{ "TitleVideo", "TEXT" },
				{ "Description", "TEXT" },
				{ "TimeCreate", "TEXT" },
				{ "TimeExport", "TEXT" },
			};
		}

		public int Id { get; set; }
		public string Path { get; set; } // путь к видео файлу
		public string Extension { get; set; } // Расширение видео файла
		public string NameVideo { get; set; } // Название видео файла
		public string TitleVideo { get; set; } // Название видео
		public string Description { get; set; } // Описание видео
		public DateTime TimeCreate { get; set; } // Время создания видео файла
		public DateTime TimeExport { get; set; } // Время загрузки видео файла в приложение

		public override List<ClipTable> AdapterTable(DataTable dataTable)
		{
			List<ClipTable> listVideos = new List<ClipTable>();

			foreach (var item in dataTable.AsEnumerable())
			{
				listVideos.Add(new ClipTable()
				{
					Id = Convert.ToInt32(item[0]),
					Path = item[1].ToString(),
					Extension = item[2].ToString(),
					NameVideo = item[3].ToString(),
					TitleVideo = item[4].ToString(),
					Description = item[5].ToString(),
					TimeCreate = DateTime.Parse(item[6].ToString()),
					TimeExport = DateTime.Parse(item[7].ToString()),
				});
			}

			return listVideos;
		}
	}
}
