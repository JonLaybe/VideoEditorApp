using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	/// "Слайдер для обрезки видео кадров"
	/// </summary>
	public partial class SliderCropControl : UserControl
	{
		public SliderCropControl()
		{
			InitializeComponent();
		}

		public int Maximum
		{
			get { return (int)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(int), typeof(SliderCropControl), new PropertyMetadata(0,
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (SliderCropControl)d;
					control.sliderStart.Maximum = (int)e.NewValue;
					control.sliderEnd.Maximum = (int)e.NewValue;
				}));

		public int Minimum
		{
			get { return (int)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public static readonly DependencyProperty MinimumProperty =
			DependencyProperty.Register("Minimum", typeof(int), typeof(SliderCropControl), new PropertyMetadata(0,
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (SliderCropControl)d;
					control.sliderStart.Minimum = (int)e.NewValue;
					control.sliderEnd.Minimum = (int)e.NewValue;
				}));

		public int ValueStart
		{
			get { return (int)GetValue(ValueStartProperty); }
			set { SetValue(ValueStartProperty, value); }
		}
		public static readonly DependencyProperty ValueStartProperty =
			DependencyProperty.Register("ValueStart", typeof(int), typeof(SliderCropControl), new PropertyMetadata(0,
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (SliderCropControl)d;
					control.sliderStart.Value = (int)e.NewValue;
				}));

		public int ValueEnd
		{
			get { return (int)GetValue(ValueEndProperty); }
			set { SetValue(ValueEndProperty, value); }
		}
		public static readonly DependencyProperty ValueEndProperty =
			DependencyProperty.Register("ValueEnd", typeof(int), typeof(SliderCropControl), new PropertyMetadata(0,
				(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
				{
					var control = (SliderCropControl)d;
					control.sliderEnd.Value = (int)e.NewValue;
				}));

		/*public ObservableCollection<BitmapImage> ItemSource
		{
			get { return (ObservableCollection<BitmapImage>)GetValue(ItemSourceProperty); }
			set { SetValue(ItemSourceProperty, value); }
		}
		public static readonly DependencyProperty ItemSourceProperty =
			DependencyProperty.Register("ItemSource", typeof(ObservableCollection<BitmapImage>), typeof(SliderCropControl), new PropertyMetadata(null, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			{
				var control = (SliderCropControl)d;
				control.listFrames.ItemsSource = (ObservableCollection<BitmapImage>)e.NewValue;
			}));*/

		//Change Property value
		// Change Slider Value

		private void sliderStart_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (sliderEnd.Value < e.NewValue)
				sliderStart.Value = e.OldValue;
			else if (e.NewValue != e.OldValue)
				ValueStart = (int)e.NewValue;
		}

		private void sliderEnd_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (sliderStart.Value > e.NewValue)
				sliderEnd.Value = e.OldValue;
			else if (e.NewValue != e.OldValue)
				ValueEnd = (int)e.NewValue;
		}
	}
}
