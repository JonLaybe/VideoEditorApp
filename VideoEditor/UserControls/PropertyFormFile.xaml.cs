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

namespace VideoEditor.UserControls
{
	/// <summary>
	/// Логика взаимодействия для PropertyFormFile.xaml
	/// </summary>
	public partial class PropertyFormFile : UserControl
	{
		public PropertyFormFile()
		{
			InitializeComponent();
		}

		public ICommand OpenCommand // Команда "открыть"
		{
			get { return (ICommand)GetValue(OpenCommandProperty); }
			set { SetValue(OpenCommandProperty, value); }
		}
		public static readonly DependencyProperty OpenCommandProperty =
			DependencyProperty.Register("OpenCommand", typeof(ICommand), typeof(PropertyFormFile), new PropertyMetadata(
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (PropertyFormFile)d;
					control.openBtn.Command = (ICommand)e.NewValue;
				}));
		public object OpenCommandParameter // Параметр команды "открыть"
		{
			get { return (object)GetValue(OpenCommandParameterProperty); }
			set { SetValue(OpenCommandParameterProperty, value); }
		}
		public static readonly DependencyProperty OpenCommandParameterProperty =
			DependencyProperty.Register("OpenCommandParameter", typeof(object), typeof(PropertyFormFile));

		public ICommand DeleteCommand // Команда "Удалить"
		{
			get { return (ICommand)GetValue(DeleteCommandProperty); }
			set { SetValue(DeleteCommandProperty, value); }
		}
		public static readonly DependencyProperty DeleteCommandProperty =
			DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(PropertyFormFile), new PropertyMetadata(
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (PropertyFormFile)d;
					control.deleteBtn.Command = (ICommand)e.NewValue;
				}));
		public object DeleteCommandParameter // Параметр команды "Удалить"
		{
			get { return (object)GetValue(DeleteCommandParameterProperty); }
			set { SetValue(DeleteCommandParameterProperty, value); }
		}
		public static readonly DependencyProperty DeleteCommandParameterProperty =
			DependencyProperty.Register("DeleteCommandParameter", typeof(object), typeof(PropertyFormFile), new PropertyMetadata(
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (PropertyFormFile)d;
					control.deleteBtn.CommandParameter = (object)e.NewValue;
				}));

		public ICommand PropertyCommand // Команда "Свойства"
		{
			get { return (ICommand)GetValue(PropertyCommandProperty); }
			set { SetValue(PropertyCommandProperty, value); }
		}
		public static readonly DependencyProperty PropertyCommandProperty =
			DependencyProperty.Register("PropertyCommand", typeof(ICommand), typeof(PropertyFormFile), new PropertyMetadata(
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (PropertyFormFile)d;
					control.propertyBtn.Command = (ICommand)e.NewValue;
				}));
		public object PropertyCommandParameter // Параметр команды "Свойства"
		{
			get { return (object)GetValue(PropertyCommandParameterProperty); }
			set { SetValue(PropertyCommandParameterProperty, value); }
		}
		public static readonly DependencyProperty PropertyCommandParameterProperty =
			DependencyProperty.Register("PropertyCommandParameter", typeof(object), typeof(PropertyFormFile));
	}
}
