using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
	public partial class MediaElementControl : UserControl
	{
		private DispatcherTimer _positionTimer;

		public MediaElementControl()
		{
			InitializeComponent();
			_positionTimer = new DispatcherTimer();
			_positionTimer.Interval += TimeSpan.FromMilliseconds(1);
			_positionTimer.Tick += PositionTimer_Tik;

			mediaElement.ScrubbingEnabled = true;
			IsPlay = true;
			IsPlay = false;
		}

		public bool IsPlay
		{
			get { return (bool)GetValue(IsPlayProperty); }
			set { SetValue(IsPlayProperty, value); }
		}
		public static readonly DependencyProperty IsPlayProperty = DependencyProperty.Register("IsPlay", typeof(bool), typeof(MediaElementControl), new PropertyMetadata(false, (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
		{
			var control = (MediaElementControl)d;
			control.UpdatePlaybackState();
		}));

		public string Source
		{
			get { return (string)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}
		public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(string), typeof(MediaElementControl), new PropertyMetadata("", (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
		{
			var control = (MediaElementControl)d;
			control.UpdateSource(e.NewValue.ToString());
		}));

		public TimeSpan DurationTime // Длительность видео файла
		{
			get { return (TimeSpan)GetValue(DurationTimeProperty); }
			set { SetValue(DurationTimeProperty, value); }
		}
		public static readonly DependencyProperty DurationTimeProperty = DependencyProperty.Register("DurationTime", typeof(TimeSpan), typeof(MediaElementControl), new PropertyMetadata(null));

		public TimeSpan CurrentTime // Текущее время
		{
			get { return (TimeSpan)GetValue(CurrentTimeProperty); }
			set { SetValue(CurrentTimeProperty, value); }
		}
		public static readonly DependencyProperty CurrentTimeProperty = DependencyProperty.Register("CurrentTime", typeof(TimeSpan), typeof(MediaElementControl), new PropertyMetadata(null));

		private void PositionTimer_Tik(object sender, EventArgs e) // event tik timer
		{
			CurrentTime = mediaElement.Position;
		}

		public void StopPlayer()
		{
			_positionTimer.Stop();
			mediaElement.Stop();
			IsPlay = false;
		}

		//ChangeValue
		private void UpdatePlaybackState()
		{
			if (IsPlay)
			{
				_positionTimer.Start();
				mediaElement.Play();
			}
			else
			{
				_positionTimer.Stop();
				mediaElement.Pause();
			}
		}

		public void SetPosition(TimeSpan timePosition) // Установить позицию проигрывателя
		{
			mediaElement.Position = timePosition;
			CurrentTime = timePosition;
		}

		private void UpdateSource(string source) // Обновление пути видео
		{
			try
			{
				mediaElement.Source = new Uri(source);
			}
			catch { }
		}

		//Event Mediaplayer
		private void MediaElement_MediaOpened(object sender, RoutedEventArgs e) // event при открытии видео файла
		{
			DurationTime = mediaElement.NaturalDuration.TimeSpan;
		}

		private void mediaElement_MediaEnded(object sender, RoutedEventArgs e) // event при закрытии видео файла
		{
			if (mediaElement != null)
			{
				mediaElement.Stop();
				IsPlay = false;
			}
		}

		private void mediaElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			IsPlay = !IsPlay;
			//MessageBox.Show(mediaElement.Position.ToString());
		}
	}
}