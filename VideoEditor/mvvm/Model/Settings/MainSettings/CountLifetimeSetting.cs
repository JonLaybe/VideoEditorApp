using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.Settings.Interfaces;

namespace VideoEditor.mvvm.Model.Settings.MainSettings
{
	public class CountLifetimeSetting : Setting // Настройка счетчика дней жизни
	{
		public int CountDays { get; set; } // Дни жизни файла

		public CountLifetimeSetting() : base("CountLifetime", "Counts")
		{
			CountDays = 0;
		}

		// TODO сделать применение черпез интерфейс Notify
		public override void ApplaySetting()
		{

		}

		public override void MakeyDefaultSetting()
		{
			CountDays = 7;
		}
	}
}
