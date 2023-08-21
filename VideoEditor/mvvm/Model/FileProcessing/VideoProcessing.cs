using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using VideoEditor.mvvm.Model.FileProcessing.Interface;
using VideoEditor.mvvm.Model.WorkingWithFile.Files.Video;
using OpenCvSharp;
using System.Drawing;

namespace VideoEditor.mvvm.Model.FileProcessing
{
	public class VideoProcessing : IFileProcessing // класс для обработки видео
	{
		public VideoFile Video { get; private set; }
		public int CountFrameResult { get; private set; }
		public double DelayBetweenFrames { get; private set; } // задержка между кадрами
		private VideoCapture _capture;

		public VideoProcessing(VideoFile videoFile)
		{
			Video = videoFile;

			_capture = new VideoCapture(videoFile.Path);
			CountFrameResult = (int)(_capture.Fps / 3);
			DelayBetweenFrames = 1000.0 / _capture.Fps;
		}

		public int CountFrame
		{
			get { return _capture.FrameCount; }
		}

		public int GetCountFrame() => _capture.FrameCount;

		public async Task<List<BitmapImage>> GetFrameVideoAsync() // Получение кадров из видео
		{
			List<BitmapImage> listImages = new List<BitmapImage>();

			int position = 0;
			var image = new Mat();
			for (int i = 0; i < (int)(CountFrame/_capture.Fps); i++) //Math.Ceiling
			{
				for (int j = 0; j < 3; j++)
				{
					_capture.PosFrames = position;
					await Task.Run(() => _capture.Read(image));
					if (image.Empty())
					{
						break;
					}
					BitmapImage bitmapImage = ConvertBitmapImage.Convert(image.ToMemoryStream());
					bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
					listImages.Add(ConvertBitmapImage.Convert(image.ToMemoryStream()));
					position += CountFrameResult;
				}
			}

			return listImages;
		}

		public VideoFile VideoCropping(string path, int TrimFrameFromStart, int TrimFrameFromEnd) // Обрезка видео
		{
			// Проверяем, успешно ли загружено видео
			if (!_capture.IsOpened())
				throw new Exception("VideoProcessingError: function VideoCropping error failed to open video file.");

			// Получаем информацию о видео
			int frameWidth = _capture.FrameWidth;
			int frameHeight = _capture.FrameHeight;
			int frameCount = _capture.FrameCount;

			// Начальный и конечный кадры для обрезки
			int startFrame = TrimFrameFromStart; // Измените значение, чтобы указать начальный кадр обрезки
			int endFrame = TrimFrameFromEnd; // Измените значение, чтобы указать конечный кадр обрезки

			// Создаем VideoWriter для записи обрезанного видео
			VideoWriter writer = new VideoWriter(path, VideoWriter.FourCC('X', 'V', 'I', 'D'), _capture.Fps, new OpenCvSharp.Size(frameWidth, frameHeight), true);

			// Переходим к начальному кадру
			_capture.PosFrames = startFrame;

			// Обрезаем видео и сохраняем каждый кадр в VideoWriter
			for (int frameIndex = startFrame; frameIndex < endFrame; frameIndex++)
			{
				Mat frame = new Mat();
				_capture.Read(frame); // Считываем текущий кадр
				if (!_capture.IsOpened())
					break; // Выходим из цикла, если достигли конца видео

				writer.Write(frame); // Записываем обработанный кадр в выходное видео
			}
			// Освобождаем ресурсы
			writer.Dispose();
			return new VideoFile(path);
		}

		public void Processing()
		{
			throw new NotImplementedException();
		}
	}
}
