using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model.Enums;

namespace VideoEditor.mvvm.Model.Settings.MainSettings
{
	public class ThemeSetting : Setting
	{
		public Themes Theme { get; set; }

		public ThemeSetting() : base("InstalledTheme", "ThemeApp")
		{

		}

		public override void ApplaySetting()
		{
			AppTheme.ChangeTheme(Theme == Themes.Light ? new Uri("Style\\Themes\\Light.xaml", UriKind.Relative) :
				new Uri("Style\\Themes\\Dark.xaml", UriKind.Relative));
		}

		public override void MakeyDefaultSetting()
		{
			Theme = Themes.Light;
		}
	}
}
