using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Db.WorkingWithDataBase
{
	public class GeneralModel<T> where T : GeneralTable<T>, new() // Класс для работы с подключенной базой данной
	{
		private ConnactDb _connactDb;
		protected CommandDb _commandDb;
		public T Table { get; set; } // сущность

		public GeneralModel(ConnactDb connact)
		{
			_connactDb = (ConnactDb)connact.Clone();
			_commandDb = new CommandDb(_connactDb);
			Table = new T();
		}

		public void CreateTable() // создание таблицы
		{
			_commandDb.CreateTable(Table.NameTable, Table.Columns);
		}
		public void Insert(string[] row_values) // добавление всех значения в бд
		{
			if (row_values.Length == Table.Columns.Count())
			{
				Dictionary<string, string> rows = new Dictionary<string, string>();
				for (int i = 0; i < Table.Count(); i++)
				{
					rows.Add(Table.Columns.ElementAt(i).Key, row_values[i]);
				}
				_commandDb.Insert(Table.NameTable, rows);
			}
			else
			{
				throw new Exception("The number of elements in the array is not equal to the column table");
			}
		}
		public void InsertAsync(string[] row_values) => Task.Run(() => Insert(row_values)); // добавление всех значения в бд асинхронно
		public void InsertAsync(Dictionary<string, string> row_values) => Task.Run(() => _commandDb.Insert(Table.NameTable, row_values)); // добавление выборочных значений в бд
		// TODO проверка на правильность key и length key
		public void Insert(Dictionary<string, string> row_values) // добавление выборочных значений в бд
		{
			_commandDb.Insert(Table.NameTable, row_values);
		}
		public List<T> GetAllTable() // получение всей таблицы
		{
			return Table.AdapterTable(_commandDb.GetAllTable(Table.NameTable));
		}
		public List<T> Select(string query) // создание запроса
		{
			return Table.AdapterTable(_commandDb.SelectTable(query));
		}
		public void DeleteById(int id)
		{
			_commandDb.Execute($"DELETE FROM {Table.NameTable} WHERE id = {id};");
		}
	}
}
