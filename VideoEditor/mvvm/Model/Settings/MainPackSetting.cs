using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using VideoEditor.mvvm.Model.CheckingDirectoryAndFile.CheckingFile;

namespace VideoEditor.mvvm.Model.Settings
{
	// Упаковывает все настройки
	public class MainPackSetting : ConfigPack
	{
		public MainPack Pack { get; private set; }
		private static MainPackSetting _mainPackSetting;

		private MainPackSetting()
		{
			Pack = new MainPack();
		}

		public MainPack FillPack() // Заполненеи настроек
		{
			DefaultOperationsFile defaultOperationsFile = new DefaultOperationsFile();

			if (!defaultOperationsFile.IsExistsAndCreate(NameFile)) // если файла с настройками нет
			{
                Pack.MakeDefaultSettings(); // применение стандартных настроек
				using(StreamWriter stram = new StreamWriter(NameFile))
				{
					stram.Write(JsonConvert.SerializeObject(Pack));
				}
			}

			using (StreamReader stream = new StreamReader(NameFile)) // чтение файла настроек
			{
				Pack = JsonConvert.DeserializeObject<MainPack>(stream.ReadToEnd());
			}

			Pack.ApplaySettings(); // применение настроек

			return Pack;
		}

		public static MainPackSetting GetPack() => _mainPackSetting == null ? _mainPackSetting = new MainPackSetting() : _mainPackSetting;
	}
}
