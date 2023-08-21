using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace VideoEditor.mvvm.Model
{
	public class ConvertBitmapImage // Конвертирует в BitmapImage
	{
		public static BitmapImage Convert(Bitmap bitmap)
		{
			MemoryStream ms = new MemoryStream();
			bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

			return Convert(ms);
		}
		public static BitmapImage Convert(MemoryStream msImage)
		{
			BitmapImage image = new BitmapImage();
			image.BeginInit();
			msImage.Seek(0, SeekOrigin.Begin);
			image.StreamSource = msImage;
			image.EndInit();

			return image;
		}
	}
}
