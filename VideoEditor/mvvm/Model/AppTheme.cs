using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoEditor.mvvm.Model
{
	public class AppTheme
	{
		public static void ChangeTheme(Uri theme)
		{
			ResourceDictionary Theme = new ResourceDictionary() { Source = theme };

			App.Current.Resources.Clear();
			App.Current.Resources.MergedDictionaries.Add(Theme);
		}
	}
}
