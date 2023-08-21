using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.WorkingWithFile
{
	public class GeneralFile // общицй файл
	{
		public int Id { get; set; } // уникальный идентификатор файла
		public string Path { get; set; } // путь к файлу
		public string Extension { get; set; } // Расширение файла
		public string NameFile { get; set; } // Название файла
		public DateTime TimeCreate { get; set; } // Время создания файла
		public DateTime TimeExport { get; set; } // Время экспорта файла

		public GeneralFile() { }
		public GeneralFile(string path)
		{
			Path = path;
			NameFile = System.IO.Path.GetFileNameWithoutExtension(path);
			Extension = System.IO.Path.GetExtension(path);
		}

		public virtual void Save()
		{

		}

		// TODO rename(SaveFile) и привязка данных всесто VideoFile -> GeneralFile
		public virtual string Copy(string fromPath, string dir) // сохраняет файл в проект (fromPath - файл, dir - путь для сохранения)
		{
			string newPath = $"{dir}\\{System.IO.Path.GetFileName(fromPath)}";
			Path = newPath;
			File.Copy(fromPath, newPath);

			Extension = System.IO.Path.GetExtension(newPath);
			NameFile = System.IO.Path.GetFileNameWithoutExtension(newPath);
			TimeCreate = new FileInfo(newPath).CreationTime;
			TimeExport = DateTime.Now;

			return Path;
		}
		public async Task<string> CopyAsync(string fromPath, string dir) // сохраняет файл в проект (fromPath - файл, dir - путь для сохранения)
		{
			Task<string> taskSave = Task.Run(() => Copy(fromPath, dir));

			await taskSave;

			return taskSave.Result;
		}
		public virtual void DeleteFile()
		{
			File.Delete(Path);
		}
	}
}
