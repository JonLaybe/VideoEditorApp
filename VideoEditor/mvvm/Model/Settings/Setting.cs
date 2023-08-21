using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VideoEditor.mvvm.Model.Settings.Interfaces;

namespace VideoEditor.mvvm.Model.Settings
{
	public abstract class Setting : ISettings // Конвертация в Json блока настроек
	{
		public string Name { get; set; }
		public string Chapter { get; set; }

		public Setting(string name, string chapter)
		{
			Name = name;
			Chapter = chapter;
		}

		public string JsonSerializeConvert<T>(T obj) => JsonConvert.SerializeObject(obj);
		public abstract void MakeyDefaultSetting(); // применение первых настроек
		public abstract void ApplaySetting(); // действие
	}
}