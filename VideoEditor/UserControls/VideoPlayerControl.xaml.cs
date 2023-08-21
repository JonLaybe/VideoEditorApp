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

namespace VideoEditor.UserControls
{
	/// <summary>
	/// Логика взаимодействия для VideoPlayerControl.xaml
	/// </summary>
	public partial class VideoPlayerControl : UserControl
	{
		public VideoPlayerControl()
		{
			InitializeComponent();
		}

		public string Source
		{
			get { return (string)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}
		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(VideoPlayerControl), new PropertyMetadata((DependencyObject d, DependencyPropertyChangedEventArgs e) => {
			var control = (VideoPlayerControl)d;
			if (e.NewValue != null)
				control.ChangeSourseProperty(e.NewValue.ToString());
		}));

		public double MinimumValue
		{
			get { return (double)GetValue(MinimumValueProperty); }
			set { SetValue(MinimumValueProperty, value); }
		}
		public static readonly DependencyProperty MinimumValueProperty =
			DependencyProperty.Register("MinimumValue", typeof(double), typeof(VideoPlayerControl), new PropertyMetadata((DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			{
				var control = (VideoPlayerControl)d;
				control.sliderInVideo.Minimum = (double)e.NewValue;
			}));

		public double MaximumValue
		{
			get { return (double)GetValue(MaximumValueProperty); }
			set { SetValue(MaximumValueProperty, value); }
		}
		public static readonly DependencyProperty MaximumValueProperty =
			DependencyProperty.Register("MaximumValue", typeof(double), typeof(VideoPlayerControl), new PropertyMetadata((DependencyObject d, DependencyPropertyChangedEventArgs e) =>
			{
				var control = (VideoPlayerControl)d;
				control.sliderInVideo.Maximum = (double)e.NewValue;
			}));

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			mediaElement.IsPlay = !mediaElement.IsPlay;
		}

		//Change property

		private void ChangeSourseProperty(string source)
		{
			mediaElement.Source = source;
		}

		private bool _oldValueIsPlay;
		private void sliderInVideo_ChangeValueSlider(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (sliderInVideo.Maximum == (double)e.NewValue)
			{
				mediaElement.StopPlayer();
				_oldValueIsPlay = false;
			}
			else if (mediaElement.CurrentTime.TotalMilliseconds != e.NewValue)
			{
				if (mediaElement.IsPlay)
				{
					_oldValueIsPlay = true;
					mediaElement.IsPlay = false;
				}
				else
				{
					_oldValueIsPlay = false;
				}
				mediaElement.SetPosition(TimeSpan.FromMilliseconds(e.NewValue));
			}
			else if (!mediaElement.IsPlay)
				if (_oldValueIsPlay)
					mediaElement.IsPlay = true;
		}
	}
}
