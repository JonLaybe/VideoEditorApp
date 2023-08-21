using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VideoEditor.mvvm.Model.WorkingWithFile.Interface;

namespace VideoEditor.mvvm.Model.WorkingWithFile.Lifetime
{
	public class FileLifetime // время жизни файла
	{
		private List<GeneralFile> _listFile;
		private List<INotifyDeleteFile> _listNotifyFileLifetimes;

		public FileLifetime()
		{
			_listFile = new List<GeneralFile>();
			_listNotifyFileLifetimes = new List<INotifyDeleteFile>();
		}

		public void AppendNotify(INotifyDeleteFile notifyFileLifetime)
		{
			_listNotifyFileLifetimes.Add(notifyFileLifetime);
		}

		public void AppendNotify(List<INotifyDeleteFile> notifyFileLifetime)
		{
			foreach (var item in notifyFileLifetime)
				_listNotifyFileLifetimes.Add(item);
		}

		public void ClearFile() => _listFile.Clear();

		private void NotifySubscriptions(GeneralFile file)
		{
			foreach (var item in _listNotifyFileLifetimes)
				item.OnFileDelete(file);
		}

		private List<GeneralFile> GetClone(List<GeneralFile> listGeneralFiles) => listGeneralFiles.GetRange(0, listGeneralFiles.Count); // создание копии листа всех файлов

		private void DeleteFile(GeneralFile file)
		{
			_listFile.Remove(file);
			File.Delete(file.Path);
			NotifySubscriptions(file);
		}

		public void AppendFile(GeneralFile file) => _listFile.Add(file);

		public void AppendFile(List<GeneralFile> files)
		{
			foreach (var item in files)
				_listFile.Add(item);
		}

		//TODO реализовать copy
		public List<GeneralFile> GetFiles() => GetClone(_listFile);

		// TODO сделать настройку удалять по времени экспорта или по времени создания
		public int StartChecking(int dayLifeTime) // запускает проверку файлов(lifeTime - число дней)
		{
			int deleteFileCount = 0;

			foreach (var item in GetClone(_listFile))
			{
				if (item.TimeExport.AddDays(dayLifeTime) <= DateTime.Now)
				{
					DeleteFile(item);
					deleteFileCount++;
				}
			}

			return deleteFileCount;
		}
	}
}
