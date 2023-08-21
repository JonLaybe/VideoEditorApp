using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;

namespace VideoEditor.mvvm.Model.WorkingWithFile
{
	public class AdapterFile<T> : GeneralFile where T : GeneralFile, new() // Адаптирует лист GeneralFile в лист классов наследуемый от GeneralFile
	{
		//public List<T> ListAdapter(List<GeneralFile> generalFiles) => (from item in generalFiles select (T)item).ToList();
		public List<T> ListAdapter(List<GeneralFile> generalFiles)
		{
			List<T> result = new List<T>();
			foreach (var item in generalFiles)
			{
				result.Add(new T()
				{
					Id = item.Id,
					Path = item.Path,
					Extension = item.Extension,
					NameFile = item.NameFile,
					TimeCreate = item.TimeCreate,
					TimeExport = item.TimeExport,
				});
			}

			return result;
		}
	}
}