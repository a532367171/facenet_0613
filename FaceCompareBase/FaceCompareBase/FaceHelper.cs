using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
namespace FaceCompareBase
{
	public class FaceHelper
	{
		public static int MC_BetweenEyes(FaceModel faceModel)
		{
			return faceModel.RightEyeFacePoint.X - faceModel.LeftEyeFacePoint.X;
		}
		public static Point[] MC_GetFacePoints(FaceModel faceModel)
		{
			return new Point[]
			{
				FaceHelper.GetPointByPoint(faceModel.LeftEyeFacePoint),
				FaceHelper.GetPointByPoint(faceModel.RightEyeFacePoint),
				FaceHelper.GetPointByPoint(faceModel.Nose),
				FaceHelper.GetPointByPoint(faceModel.LeftMouthFacePoint),
				FaceHelper.GetPointByPoint(faceModel.RightMouthFacePoint)
			};
		}
		private static Point GetPointByPoint(FaceModelPoint facePoint)
		{
			return new Point(facePoint.X, facePoint.Y);
		}
		public static Rectangle MC_GetRectangleByRect(FaceModelRect rect)
		{
			return new Rectangle
			{
				X = rect.Left,
				Y = rect.Top,
				Width = rect.Right - rect.Left,
				Height = rect.Bottom - rect.Top
			};
		}
		public static Rectangle MC_GetRectangleByRect(FaceModelRect rect, Image<Bgr, byte> image)
		{
			Rectangle rectangle;
			lock (rect)
			{
				rectangle = default(Rectangle);
				rectangle.X = rect.Left;
				rectangle.Y = rect.Top;
				rectangle.Width = rect.Right - rect.Left;
				rectangle.Height = rect.Bottom - rect.Top;
				Rectangle rectangle2 = rectangle;
				rectangle2.X -= rectangle2.Width / 2;
				rectangle2.Y -= rectangle2.Height / 2;
				rectangle2.Width *= 2;
				rectangle2.Height *= 2;
				if (rectangle2.X < 0)
				{
					rectangle2.X = 0;
				}
				if (rectangle2.Y < 0)
				{
					rectangle2.Y = 0;
				}
				if (rectangle2.Width > image.Width - rectangle2.X)
				{
					rectangle2.Width = image.Width - rectangle2.X;
				}
				if (rectangle2.Height > image.Height - rectangle2.Y)
				{
					rectangle2.Height = image.Height - rectangle2.Y;
				}
				rectangle = rectangle2;
			}
			return rectangle;
		}
		public static Rectangle MC_GetRectangleByRect(FaceModelRect rect, Size size)
		{
			Rectangle result = new Rectangle
			{
				X = rect.Left,
				Y = rect.Top,
				Width = rect.Right - rect.Left,
				Height = rect.Bottom - rect.Top
			};
			result.X -= result.Width / 2;
			result.Y -= result.Height / 2;
			result.Width *= 2;
			result.Height *= 2;
			if (result.X < 0)
			{
				result.X = 0;
			}
			if (result.Y < 0)
			{
				result.Y = 0;
			}
			if (result.Width > size.Width - result.X)
			{
				result.Width = size.Width - result.X;
			}
			if (result.Height > size.Height - result.Y)
			{
				result.Height = size.Height - result.Y;
			}
			return result;
		}
	}
}
