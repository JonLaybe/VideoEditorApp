using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Interface;

namespace VideoEditor.mvvm.Model.Db.WorkingWithDataBase
{
	public class ContextModelDataBase : IContextModelDataBase // Класс для работы с подключениями sqlite базы данных
	{
		private List<ConnactDb> _listConnecting; // все подключения к базам данных (название бд, подключение к бд)
		private static ContextModelDataBase _contextModelDataBase;

		private ContextModelDataBase()
		{
			_listConnecting = new List<ConnactDb>();
		}

		public ConnactDb ElementAt(string nameDb) // получить элемент ConnactDb(подключение к бд) (nameDb - название базы данных)
		{
			return (from item in _listConnecting where item.NameDb == nameDb select item).LastOrDefault();
		}
		public ConnactDb ElementAt(ConnactDb connactDb) => ElementAt(connactDb.NameDb); // получить элемент ConnactDb(подключение к бд) (nameDb - название базы данных)

		public void CreatingConnecting(string nameDb) // создание подключения
		{
			AppendConnecting(new ConnactDb(nameDb));
		}

		public void AppendConnecting(ConnactDb connectDb) // добавление подключения
		{
			CheckNameDb(connectDb.NameDb);
			_listConnecting.Add(connectDb);
		}

		public void DeleteConnecting(string nameDb) // Удаление подключения
		{
			ConnactDb connactDb = (from item in _listConnecting where item.NameDb == nameDb select item).LastOrDefault();
			connactDb.ConnectionModel.Close();
		}

		private List<ConnactDb> CheckNameDb(string nameDb) // Провеяет есть ли в лите подключений подключенная база данных с таким же именем
		{
			List<ConnactDb> listConnecting = (from item in _listConnecting where item.NameDb == nameDb select item).ToList();

			if (listConnecting.Count() == 0)
				return listConnecting;

			throw new Exception("ContextModelDataBaseError: A database name with the same name already exists");
		}

		public void DeleteConnecting(ConnactDb connactDb) => DeleteConnecting(connactDb.NameDb); // Удаление подключения

		public static ContextModelDataBase GetContextModelDataBase() => _contextModelDataBase == null ? _contextModelDataBase = new ContextModelDataBase() : _contextModelDataBase;
	}
}
