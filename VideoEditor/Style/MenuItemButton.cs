using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoEditor.Style
{
	public class MenuItemButton : Button
	{
		static MenuItemButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItemButton), new FrameworkPropertyMetadata(typeof(MenuItemButton)));
		}

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MenuItemButton), new PropertyMetadata(null));

		public static readonly DependencyProperty ColorSelectedItemProperty =
			DependencyProperty.Register("ColorSelectedItem", typeof(System.Drawing.Color), typeof(MenuItemButton));

		public SolidColorBrush SelectedItemColor
		{
			get { return (SolidColorBrush)GetValue(SelectedItemColorProperty); }
			set { SetValue(SelectedItemColorProperty, value); }
		}
		public static readonly DependencyProperty SelectedItemColorProperty =
			DependencyProperty.Register("SelectedItemColor", typeof(SolidColorBrush), typeof(MenuItemButton));

		public Geometry Icon
		{
			get { return (Geometry)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Geometry), typeof(MenuItemButton), new PropertyMetadata(null));
	}
}
