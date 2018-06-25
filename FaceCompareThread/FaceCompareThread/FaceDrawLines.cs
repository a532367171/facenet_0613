using FaceCompareBase;
using System;
using System.Drawing;
namespace FaceCompareThread
{
	public static class FaceDrawLines
	{
		public static Point[][] GetPoints(FaceModelRect _faceRect, int cutCount, Size size, Size zoomSize)
		{
			Point[][] result;
			try
			{
				FaceModelRect faceModelRect = new FaceModelRect
				{
					Top = _faceRect.Top,
					Bottom = _faceRect.Bottom,
					Left = _faceRect.Left,
					Right = _faceRect.Right
				};
				decimal d = zoomSize.Height / size.Height;
				decimal d2 = zoomSize.Width / size.Width;
				faceModelRect.Top = Convert.ToInt32(Math.Ceiling(faceModelRect.Top * d));
				faceModelRect.Bottom = Convert.ToInt32(Math.Ceiling(faceModelRect.Bottom * d));
				faceModelRect.Left = Convert.ToInt32(Math.Ceiling(faceModelRect.Left * d2));
				faceModelRect.Right = Convert.ToInt32(Math.Ceiling(faceModelRect.Right * d2));
				int num = (faceModelRect.Right - faceModelRect.Left) / cutCount;
				int num2 = (faceModelRect.Bottom - faceModelRect.Top) / cutCount;
				num = num2;
				Point[] array = new Point[]
				{
					new Point(faceModelRect.Left, faceModelRect.Top),
					new Point(faceModelRect.Left, faceModelRect.Top + num2)
				};
				Point[] array2 = new Point[]
				{
					new Point(faceModelRect.Left, faceModelRect.Bottom - num2),
					new Point(faceModelRect.Left, faceModelRect.Bottom)
				};
				Point[] array3 = new Point[]
				{
					new Point(faceModelRect.Left, faceModelRect.Bottom),
					new Point(faceModelRect.Left + num, faceModelRect.Bottom)
				};
				Point[] array4 = new Point[]
				{
					new Point(faceModelRect.Right - num, faceModelRect.Bottom),
					new Point(faceModelRect.Right, faceModelRect.Bottom)
				};
				Point[] array5 = new Point[]
				{
					new Point(faceModelRect.Right, faceModelRect.Bottom),
					new Point(faceModelRect.Right, faceModelRect.Bottom - num2)
				};
				Point[] array6 = new Point[]
				{
					new Point(faceModelRect.Right, faceModelRect.Top + num2),
					new Point(faceModelRect.Right, faceModelRect.Top)
				};
				Point[] array7 = new Point[]
				{
					new Point(faceModelRect.Right, faceModelRect.Top),
					new Point(faceModelRect.Right - num, faceModelRect.Top)
				};
				Point[] array8 = new Point[]
				{
					new Point(faceModelRect.Left + num, faceModelRect.Top),
					new Point(faceModelRect.Left, faceModelRect.Top)
				};
				result = new Point[][]
				{
					array,
					array2,
					array3,
					array4,
					array5,
					array6,
					array7,
					array8
				};
			}
			catch (Exception arg_320_0)
			{
				throw arg_320_0;
			}
			return result;
		}
		public static Point[][] GetPoints(FaceModelRect _faceRect, int cutCount, Size size)
		{
			Point[][] result;
			try
			{
				FaceModelRect faceModelRect = new FaceModelRect
				{
					Top = _faceRect.Top,
					Bottom = _faceRect.Bottom,
					Left = _faceRect.Left,
					Right = _faceRect.Right
				};
				int num = (faceModelRect.Right - faceModelRect.Left) / cutCount;
				int num2 = (faceModelRect.Bottom - faceModelRect.Top) / cutCount;
				num = num2;
				Point[] array = new Point[]
				{
					new Point(faceModelRect.Left, faceModelRect.Top),
					new Point(faceModelRect.Left, faceModelRect.Top + num2)
				};
				Point[] array2 = new Point[]
				{
					new Point(faceModelRect.Left, faceModelRect.Bottom - num2),
					new Point(faceModelRect.Left, faceModelRect.Bottom)
				};
				Point[] array3 = new Point[]
				{
					new Point(faceModelRect.Left, faceModelRect.Bottom),
					new Point(faceModelRect.Left + num, faceModelRect.Bottom)
				};
				Point[] array4 = new Point[]
				{
					new Point(faceModelRect.Right - num, faceModelRect.Bottom),
					new Point(faceModelRect.Right, faceModelRect.Bottom)
				};
				Point[] array5 = new Point[]
				{
					new Point(faceModelRect.Right, faceModelRect.Bottom),
					new Point(faceModelRect.Right, faceModelRect.Bottom - num2)
				};
				Point[] array6 = new Point[]
				{
					new Point(faceModelRect.Right, faceModelRect.Top + num2),
					new Point(faceModelRect.Right, faceModelRect.Top)
				};
				Point[] array7 = new Point[]
				{
					new Point(faceModelRect.Right, faceModelRect.Top),
					new Point(faceModelRect.Right - num, faceModelRect.Top)
				};
				Point[] array8 = new Point[]
				{
					new Point(faceModelRect.Left + num, faceModelRect.Top),
					new Point(faceModelRect.Left, faceModelRect.Top)
				};
				result = new Point[][]
				{
					array,
					array2,
					array3,
					array4,
					array5,
					array6,
					array7,
					array8
				};
			}
			catch (Exception arg_258_0)
			{
				throw arg_258_0;
			}
			return result;
		}
	}
}
