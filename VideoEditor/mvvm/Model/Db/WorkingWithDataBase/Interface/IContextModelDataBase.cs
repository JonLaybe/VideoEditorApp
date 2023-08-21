using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Db.WorkingWithDataBase.Interface
{
	public interface IContextModelDataBase
	{
		ConnactDb ElementAt(string nameDb); // получить элемент ConnactDb(подключение к бд) (nameDb - название базы данных)
		ConnactDb ElementAt(ConnactDb connactDb); // получить элемент ConnactDb(подключение к бд) (nameDb - название базы данных)
		void CreatingConnecting(string nameDb); // создание подключения
		void AppendConnecting(ConnactDb connectDb); // добавление подключения
		void DeleteConnecting(string nameDb); // Удаление подключения
		void DeleteConnecting(ConnactDb connactDb); // Удаление подключения
	}
}
