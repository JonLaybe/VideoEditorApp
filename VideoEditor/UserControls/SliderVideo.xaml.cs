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
using System.Windows.Threading;

namespace VideoEditor.UserControls
{
	public partial class SliderVideo : UserControl
	{
		public SliderVideo()
		{
			InitializeComponent();
		}

		public double Maximum
		{
			get { return (double)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(SliderVideo), new PropertyMetadata((DependencyObject d, DependencyPropertyChangedEventArgs e) =>
		{
			var control = (SliderVideo)d;
			control.slider.Maximum = (double)e.NewValue;
		}));

		public double Minimum
		{
			get { return (double)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(SliderVideo), new PropertyMetadata((DependencyObject d, DependencyPropertyChangedEventArgs e) =>
		{
			var control = (SliderVideo)d;
			control.slider.Minimum = (double)e.NewValue;
		}));

		public double ValueTime
		{
			get { return (double)GetValue(ValueTimeProperty); }
			set { SetValue(ValueTimeProperty, value); }
		}
		public static readonly DependencyProperty ValueTimeProperty = DependencyProperty.Register("ValueTime", typeof(double), typeof(SliderVideo), new PropertyMetadata(0.0, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
		{
			var control = (SliderVideo)d;
			control.GetValueTime();
		}));

		public double ChangeValue
		{
			get { return (double)GetValue(ChangeValueProperty); }
			set { SetValue(ChangeValueProperty, value); }
		}
		public static readonly DependencyProperty ChangeValueProperty = DependencyProperty.Register("ChangeValue", typeof(double), typeof(SliderVideo), new PropertyMetadata(0.0, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
		{
			var control = (SliderVideo)d;
			control.ChangeValueTime();
		}));

		//Event

		public static readonly RoutedEvent ChangeValueSliderEvent =
			EventManager.RegisterRoutedEvent("ChangeValueSlider", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(SliderVideo));

		public event RoutedPropertyChangedEventHandler<double> ChangeValueSlider
		{
			add => AddHandler(ChangeValueSliderEvent, value);
			remove => RemoveHandler(ChangeValueSliderEvent, value);
		}

		//ChangeValue

		private void GetValueTime() => ValueTime = slider.Value;

		private void ChangeValueTime()
		{
			slider.Value = ChangeValue;
		}

		private void slider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			GetValueTime();
		}

		private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			double newValue = e.NewValue;
			double oldValue = e.OldValue;
			RoutedPropertyChangedEventArgs<double> eventArgs = new RoutedPropertyChangedEventArgs<double>(oldValue, newValue)
			{
				RoutedEvent = ChangeValueSliderEvent
			};
			RaiseEvent(eventArgs);
			ValueTime = (int)slider.Value;
		}
	}
}