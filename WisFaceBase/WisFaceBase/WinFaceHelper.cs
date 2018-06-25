using System;
using System.Drawing;
namespace WisFaceBase
{
	public static class WinFaceHelper
	{
		public static Rectangle GetRectangleByWisRect(WisRect rect, Size size)
		{
			Rectangle result = new Rectangle
			{
				X = rect.Left,
				Y = rect.Top,
				Width = rect.Right - rect.Left,
				Height = rect.Bottom - rect.Top
			};
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
		public static Rectangle GetRectangleByWisRectMax(WisRect rect, Size size)
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
