using System;
using System.Collections.Generic;
using System.Drawing;
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
	public class MenuItem : RadioButton
	{
		static MenuItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuItem), new FrameworkPropertyMetadata(typeof(MenuItem)));
		}

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MenuItem), new PropertyMetadata(null));

		public static readonly DependencyProperty ColorSelectedItemProperty =
			DependencyProperty.Register("ColorSelectedItem", typeof(System.Drawing.Color), typeof(MenuItem));

		public SolidColorBrush SelectedItemColor
		{
			get { return (SolidColorBrush)GetValue(SelectedItemColorProperty); }
			set { SetValue(SelectedItemColorProperty, value); }
		}
		public static readonly DependencyProperty SelectedItemColorProperty =
			DependencyProperty.Register("SelectedItemColor", typeof(SolidColorBrush), typeof(MenuItem));

		public Geometry Icon
		{
			get { return (Geometry)GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(Geometry), typeof(MenuItem), new PropertyMetadata(null));
	}
}
