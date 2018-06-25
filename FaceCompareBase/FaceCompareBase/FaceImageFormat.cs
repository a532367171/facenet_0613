using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
namespace FaceCompareBase
{
	public class FaceImageFormat
	{
		public static Image<Bgr, byte> ImagePixelFormat(Bitmap btBitmap)
		{
			Image<Bgr, byte> image = new Image<Bgr, byte>(btBitmap.Size);
			if (btBitmap.PixelFormat == PixelFormat.Format24bppRgb)
			{
				return image;
			}
			byte[,,] data = image.Data;
			for (int i = 0; i < btBitmap.Width; i++)
			{
				for (int j = 0; j < btBitmap.Height; j++)
				{
					Color pixel = btBitmap.GetPixel(i, j);
					data[j, i, 0] = pixel.B;
					data[j, i, 1] = pixel.G;
					data[j, i, 2] = pixel.R;
				}
			}
			return image;
		}
		public static string ImgToBase64String(Bitmap bitmap)
		{
			string result;
			lock (bitmap)
			{
				try
				{
					MemoryStream memoryStream = new MemoryStream();
					bitmap.Save(memoryStream, ImageFormat.Jpeg);
					byte[] array = new byte[memoryStream.Length];
					memoryStream.Position = 0L;
					memoryStream.Read(array, 0, (int)memoryStream.Length);
					memoryStream.Close();
					result = Convert.ToBase64String(array);
				}
				catch (Exception)
				{
					result = string.Empty;
				}
			}
			return result;
		}
		public static string ImgToBase64String(Image bitmap)
		{
			string result;
			lock (bitmap)
			{
				try
				{
					MemoryStream memoryStream = new MemoryStream();
					bitmap.Save(memoryStream, ImageFormat.Jpeg);
					byte[] array = new byte[memoryStream.Length];
					memoryStream.Position = 0L;
					memoryStream.Read(array, 0, (int)memoryStream.Length);
					memoryStream.Close();
					result = Convert.ToBase64String(array);
				}
				catch (Exception)
				{
					result = string.Empty;
				}
			}
			return result;
		}
		public static Image<Bgr, byte> ResizeImage(Image<Bgr, byte> img, Size Resize, bool EqualProportion)
		{
			if (img.Width <= Resize.Width)
			{
				return img;
			}
			if (EqualProportion)
			{
				int num = img.Width / Resize.Width;
				return img.Resize(Resize.Width, Resize.Height / num, Inter.Linear);
			}
			return img.Resize(Resize.Width, Resize.Height, Inter.Nearest);
		}
		public static Bitmap KiCut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
		{
			if (b == null)
			{
				return null;
			}
			int width = b.Width;
			int height = b.Height;
			if (StartX >= width || StartY >= height)
			{
				return null;
			}
			if (StartX + iWidth > width)
			{
				iWidth = width - StartX;
			}
			if (StartY + iHeight > height)
			{
				iHeight = height - StartY;
			}
			Bitmap result;
			try
			{
				Bitmap expr_43 = new Bitmap(iWidth, iHeight, b.PixelFormat);
				Graphics expr_49 = Graphics.FromImage(expr_43);
				expr_49.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
				expr_49.Dispose();
				result = expr_43;
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public static void ImgBytesSave(byte[] bytes, string SaveFileName)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (memoryStream)
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				memoryStream.SetLength(0L);
				memoryStream.Write(bytes, 0, bytes.Length);
				Bitmap expr_30 = (Bitmap)Image.FromStream(memoryStream);
				expr_30.Save(SaveFileName, ImageFormat.Jpeg);
				expr_30.Dispose();
				memoryStream.Close();
			}
		}
		public static byte[] BitmapToByte(Bitmap bitmap)
		{
			MemoryStream memoryStream = new MemoryStream();
			new Bitmap(bitmap).Save(memoryStream, ImageFormat.Jpeg);
			byte[] arg_23_0 = memoryStream.GetBuffer();
			memoryStream.Close();
			return arg_23_0;
		}
		public static Bitmap ByteToBitmap(byte[] imgBytes)
		{
			MemoryStream memoryStream = new MemoryStream();
			Bitmap result;
			using (memoryStream)
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				memoryStream.SetLength(0L);
				memoryStream.Write(imgBytes, 0, imgBytes.Length);
				result = new Bitmap(Image.FromStream(memoryStream));
				memoryStream.Flush();
			}
			return result;
		}
		public static Image ByteArrayToImage(byte[] byteArrayIn)
		{
			if (byteArrayIn == null)
			{
				return null;
			}
			Image result;
			using (MemoryStream memoryStream = new MemoryStream(byteArrayIn))
			{
				Image arg_18_0 = Image.FromStream(memoryStream);
				memoryStream.Flush();
				result = arg_18_0;
			}
			return result;
		}
		public static Bitmap ConvertBitmapToScreen(Bitmap imageBitmap, int iBitmapWidth, int iBitmapHeight)
		{
			int width = imageBitmap.Width;
			int height = imageBitmap.Height;
			if (iBitmapHeight == 0 && iBitmapWidth == 0)
			{
				return null;
			}
			Bitmap expr_1D = new Bitmap(iBitmapWidth, iBitmapHeight);
			Graphics expr_23 = Graphics.FromImage(expr_1D);
			expr_23.SmoothingMode = SmoothingMode.HighQuality;
			expr_23.CompositingQuality = CompositingQuality.HighQuality;
			expr_23.InterpolationMode = InterpolationMode.High;
			Rectangle destRect = new Rectangle(0, 0, iBitmapWidth, iBitmapHeight);
			expr_23.DrawImage(imageBitmap, destRect, 0, 0, width, height, GraphicsUnit.Pixel);
			imageBitmap.Dispose();
			return expr_1D;
		}
		public static string ReadImageFile(string path)
		{
			string result;
			try
			{
				FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
				BinaryReader expr_0F = new BinaryReader(fileStream);
				expr_0F.BaseStream.Seek(0L, SeekOrigin.Begin);
				byte[] arg_35_0 = expr_0F.ReadBytes((int)expr_0F.BaseStream.Length);
				fileStream.Close();
				result = Convert.ToBase64String(arg_35_0);
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
