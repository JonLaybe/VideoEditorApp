using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Db.WorkingWithDataBase
{
	public abstract class GeneralTable<T> // Класс описывающиц лбщуюю таблицу
	{
		public string NameTable { get; set; } // название таблицы
		public Dictionary<string, string> Columns { get; set; } // название и тип дланных клонок

		public int Count() => Columns.Count;

		public abstract List<T> AdapterTable(DataTable dataTable); // адаптер DataTable в таблицу
	}
}
