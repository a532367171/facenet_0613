using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
namespace FaceCompareBase
{
	public class ImageClass
	{
		private static readonly object _obj = new object();
		public Image<Bgr, byte> Cut(Image<Bgr, byte> image, Rectangle rectangle)
		{
			Image<Bgr, byte> result;
			try
			{
				Monitor.Enter(ImageClass._obj);
				rectangle.X -= rectangle.Width / 2;
				rectangle.Y -= rectangle.Height / 2;
				rectangle.Width *= 2;
				rectangle.Height *= 2;
				if (rectangle.X < 0)
				{
					rectangle.X = 0;
				}
				if (rectangle.Y < 0)
				{
					rectangle.Y = 0;
				}
				if (rectangle.Width > image.Width - rectangle.X || rectangle.Width < 0)
				{
					rectangle.Width = image.Width - rectangle.X;
				}
				if (rectangle.Height > image.Height - rectangle.Y || rectangle.Height < 0)
				{
					rectangle.Height = image.Height - rectangle.Y;
				}
				Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format24bppRgb);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(image.Bitmap, new Rectangle(0, 0, rectangle.Width, rectangle.Height), rectangle, GraphicsUnit.Pixel);
					graphics.Save();
				}
				result = new Image<Bgr, byte>(bitmap);
			}
			finally
			{
				Monitor.Exit(ImageClass._obj);
			}
			return result;
		}
		public Image Cut2(Image<Bgr, byte> image, Rectangle rectangle)
		{
			Image result;
			try
			{
				Monitor.Enter(ImageClass._obj);
				rectangle.X -= rectangle.Width / 2;
				rectangle.Y -= rectangle.Height / 2;
				rectangle.Width *= 2;
				rectangle.Height *= 2;
				if (rectangle.X < 0)
				{
					rectangle.X = 0;
				}
				if (rectangle.Y < 0)
				{
					rectangle.Y = 0;
				}
				if (rectangle.Width > image.Width - rectangle.X)
				{
					rectangle.Width = image.Width - rectangle.X;
				}
				if (rectangle.Height > image.Height - rectangle.Y)
				{
					rectangle.Height = image.Height - rectangle.Y;
				}
				Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.Format24bppRgb);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(image.Bitmap, new Rectangle(0, 0, rectangle.Width, rectangle.Height), rectangle, GraphicsUnit.Pixel);
					graphics.Save();
				}
				using (MemoryStream memoryStream = new MemoryStream())
				{
					bitmap.Save(memoryStream, ImageFormat.Jpeg);
					result = Image.FromStream(memoryStream);
				}
			}
			finally
			{
				Monitor.Exit(ImageClass._obj);
			}
			return result;
		}
	}
}
