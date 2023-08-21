using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Db
{
	public class ContextDataBase : ICloneable
	{
		public string NameDb { get; set; } // имя базы даннях
		public string ConnactionString { get; set; } // строка подключения
		public SqliteConnection ConnectionModel { get; set; } // Созданное подключение

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
