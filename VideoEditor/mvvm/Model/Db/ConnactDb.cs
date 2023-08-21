using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Db
{
	public class ConnactDb : ContextDataBase // подключение к базе данных sqlite
	{
		public ConnactDb(string nameDb)
		{
			NameDb = nameDb;
			ConnactionString = $"Data Source={nameDb}.db";
			ConnectionModel = new Microsoft.Data.Sqlite.SqliteConnection(ConnactionString);
		}
	}
}
