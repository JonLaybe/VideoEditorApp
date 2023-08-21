using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using VideoEditor.mvvm.Utilites;
using VideoEditor.UserControls;

namespace VideoEditor.Style
{
	public class ListBoxFiles : ListBox
	{
		static ListBoxFiles()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxFiles), new FrameworkPropertyMetadata(typeof(ListBoxFiles)));
		}

		public ListBoxFiles()
		{
			OpenPropertyCommand = OpenPropertyBtn;
		}

		public bool IsOpenProprty // открыт ли Popup
		{
			get { return (bool)GetValue(IsOpenProprtyProperty); }
			set { SetValue(IsOpenProprtyProperty, value); }
		}
		public static readonly DependencyProperty IsOpenProprtyProperty =
			DependencyProperty.Register("IsOpenProprty", typeof(bool), typeof(ListBoxFiles), new PropertyMetadata(false,
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (ListBoxFiles)d;
					//MessageBox.Show(control.IsOpenProprty.ToString());
				}));

		public ICommand MouseDoubleClickCommand // Открытие файла
		{
			get { return (ICommand)GetValue(MouseDoubleClickCommandProperty); }
			set { SetValue(MouseDoubleClickCommandProperty, value); }
		}
		public static readonly DependencyProperty MouseDoubleClickCommandProperty =
			DependencyProperty.Register("MouseDoubleClickCommand", typeof(ICommand), typeof(ListBoxFiles));

		public ICommand OpenPropertyCommand // Открытие Popup
		{
			get { return (ICommand)GetValue(OpenPropertyCommandProperty); }
			set { SetValue(OpenPropertyCommandProperty, value); }
		}
		public static readonly DependencyProperty OpenPropertyCommandProperty =
			DependencyProperty.Register("OpenPropertyCommand", typeof(ICommand), typeof(ListBoxFiles));

		public UserControl PopupContext
		{
			get { return (UserControl)GetValue(PopupContextProperty); }
			set { SetValue(PopupContextProperty, value); }
		}
		public static readonly DependencyProperty PopupContextProperty =
			DependencyProperty.Register("PopupContext", typeof(UserControl), typeof(ListBoxFiles), new PropertyMetadata(null));

		private ICommand OpenPropertyBtn // Default OpenPropertyCommand
		{
			get
			{
				return new ButtonCommand((object obj) => {
					if (SelectedItem != null)
						IsOpenProprty = !IsOpenProprty;
					else if (IsOpenProprty)
						IsOpenProprty = false;
				});
			}
		}
	}
}
