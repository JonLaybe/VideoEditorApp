using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoEditor.mvvm.Model.Db
{
	public class CommandDb // команды к базе данных
	{
		private ContextDataBase _contextDb;

		public CommandDb(ConnactDb connact)
		{
			_contextDb = (ContextDataBase)connact.Clone();
		}

		// Сводка:
		//  Выполнение запроса
		// Параметры:
		//  query:
		//      Запрос
		// Возврат:
		//	int количество строк, иначе -1
		public int Execute(string query) // выполнение запроса
		{
			int count = 0;

			_contextDb.ConnectionModel.Open();
			SqliteCommand command = new SqliteCommand(query, _contextDb.ConnectionModel);
			count = command.ExecuteNonQuery();
			_contextDb.ConnectionModel.Close();

			return count;
		}
		// Сводка:
		//  Создает таблицу
		// Параметры:
		//  tableName:
		//      Название создаваемой таблицы
		// row_values:
		//		Словарь с именами колонок и типом данных в соответствии с синтаксисом sqlite без учета колонки id
		public void CreateTable(string tableName, Dictionary<string, string> row_values)
		{
			StringBuilder query = new StringBuilder($"CREATE TABLE IF NOT EXISTS {tableName} (id INTEGER NOT NULL PRIMARY KEY,");

			foreach (var cell in row_values)
			{
				query.Append($"{cell.Key} {cell.Value} NOT NULL,");
			}
			query.Remove(query.Length - 1, 1);
			query.Append(")");

			Execute(query.ToString());
		}
		// Сводка:
		//  Добавление данных в таблицу
		// Параметры:
		//	table:
		//		Назание таблицы в которую будут добавлятся данные
		//	row_values:
		//		Словарь с именами тех колонок в таблице и типом данных в соответствии с синтаксисом sqlite
		public void Insert(string table, Dictionary<string, string> row_values)
		{
			string query = $"INSERT INTO {table}(";

			foreach (var cell in row_values)
			{
				query += $"{cell.Key},";
			}
			query = query.Remove(query.Length - 1);
			query += ") VALUES (";
			foreach (var cell in row_values)
				query += "'" + cell.Value + "',";
			query = query.Remove(query.Length - 1);
			query += ")";
			Execute(query);
		}
		// Сводка:
		//	Выполняет Select запрос
		// Параметры:
		//  request:
		//      Запрос select
		// Возврат:
		//	Экземпляр DataTable(таблицу)
		private DataTable ModelSelect(string request)
		{
			_contextDb.ConnectionModel.Open();
			SqliteCommand command = new SqliteCommand(request, _contextDb.ConnectionModel);
			SqliteDataReader dataReader = command.ExecuteReader();
			DataTable table = new DataTable();
			for (int i = 0; i < dataReader.FieldCount; i++)
			{
				table.Columns.Add(dataReader.GetSchemaTable().Rows[i].ItemArray[0].ToString());
			}
			while (dataReader.Read())
			{
				table.Rows.Add();
				object[] obj = new object[dataReader.FieldCount];
				for (int i = 0; i < dataReader.FieldCount; i++)
				{
					obj[i] = dataReader.GetString(i);
				}
				table.Rows[table.Rows.Count - 1].ItemArray = obj;
			}
			_contextDb.ConnectionModel.Close();

			return table;
		}
		// Сводка:
		//  Получение всей таблицы
		// Параметры:
		//  tablename:
		//      Название таблицы
		// Возврат:
		//	Экземпляр DataTable(таблицу)

		public DataTable GetAllTable(string tablename)
		{
			return ModelSelect($"Select * from {tablename}");
		}
		// Сводка:
		//  Выполняет Select запрос
		// Параметры:
		//  request:
		//      Запрос select 
		// Возврат:
		//	Экземпляр DataTable(таблицу)
		public DataTable SelectTable(string request)
		{
			return ModelSelect(request);
		}
	}
}
