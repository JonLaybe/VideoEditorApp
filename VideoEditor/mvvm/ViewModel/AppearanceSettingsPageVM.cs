using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoEditor.mvvm.Model;
using VideoEditor.mvvm.Model.Enums;
using VideoEditor.mvvm.Utilites;

namespace VideoEditor.mvvm.ViewModel
{
	public class AppearanceSettingsPageVM : ViewModelBase
	{
		private List<Themes> _listThemes;
		private Themes _selectedTheme;

		public AppearanceSettingsPageVM()
		{
			ListThemes = new List<Themes>() { Themes.Light, Themes.Dark };
		}

		public List<Themes> ListThemes
		{
			get { return _listThemes; }
			set
			{
				_listThemes = value;
				Notify(nameof(ListThemes));
			}
		}

		public Themes SelectedTheme
		{
			get { return _selectedTheme; }
			set
			{
				_selectedTheme = value;
				Notify(nameof(SelectedTheme));
				if (value == Themes.Light)
					AppTheme.ChangeTheme(new Uri("Style\\Themes\\Light.xaml", UriKind.Relative));
				else if (value == Themes.Dark)
					AppTheme.ChangeTheme(new Uri("Style\\Themes\\Dark.xaml", UriKind.Relative));
			}
		}
	}
}
