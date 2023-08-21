using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VideoEditor.mvvm.Model.Settings.MainSettings;

namespace VideoEditor.mvvm.Model.Settings
{
	public class MainPack
	{
		public DefaultDirectory DefaultDirectory { get; set; }
		public CountLifetimeSetting CountLifetimeSetting { get; set; }
		public ThemeSetting ThemeSetting { get; set; }
		[JsonIgnore]
		public List<Setting> ListSettings { get; private set; }

        public MainPack()
		{
			DefaultDirectory = new DefaultDirectory();
			CountLifetimeSetting = new CountLifetimeSetting();
			ThemeSetting = new ThemeSetting();
			ListSettings = new List<Setting>
			{
				DefaultDirectory,
				CountLifetimeSetting,
				ThemeSetting,
			};

        }

		public void MakeDefaultSettings()
		{
			foreach (var item in ListSettings)
			{
				item.MakeyDefaultSetting();
			}
		}

		public void ApplaySettings()
		{
			foreach (var item in ListSettings)
			{
				item.ApplaySetting();
			}
		}
	}
}
