using System;
using System.Runtime.InteropServices;
namespace FaceDetectiveCtl.UnitBase
{
	public class DirectShowImageBase
	{
		private IntPtr _direceHandle = IntPtr.Zero;
		[DllImport("DirectDrawImage.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr Create(IntPtr intPtr);
		[DllImport("DirectDrawImage.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void Render(IntPtr intPtr, byte[] imgRgb24, int width, int height, int pith);
		[DllImport("DirectDrawImage.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void HwndReSize(IntPtr intPtr, int width, int height);
		[DllImport("DirectDrawImage.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void RenderRect(IntPtr intPtr, D2D1_RECT rect, int width, int height, int conW, int conH);
		[DllImport("DirectDrawImage.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Free")]
		private static extern void Release(IntPtr intPtr);
		public IntPtr CreateDirect(IntPtr intPtr)
		{
			if (this._direceHandle == IntPtr.Zero)
			{
				this._direceHandle = DirectShowImageBase.Create(intPtr);
				Console.WriteLine(string.Format("CreateDirect Sucess {0}", this._direceHandle));
				return this._direceHandle;
			}
			return this._direceHandle;
		}
		public void Render(byte[] imgRgb24, int width, int height, int pith)
		{
			if (this._direceHandle != IntPtr.Zero)
			{
				DirectShowImageBase.Render(this._direceHandle, imgRgb24, width, height, pith);
			}
		}
		public void HwndReSize(int width, int height)
		{
			if (this._direceHandle != IntPtr.Zero)
			{
				DirectShowImageBase.HwndReSize(this._direceHandle, width, height);
			}
		}
		public void RenderRect(D2D1_RECT rect, int width, int height, int conW, int conH)
		{
			if (this._direceHandle != IntPtr.Zero)
			{
				DirectShowImageBase.RenderRect(this._direceHandle, rect, width, height, conW, conH);
			}
		}
		public void Dispose()
		{
			if (this._direceHandle != IntPtr.Zero)
			{
				DirectShowImageBase.Release(this._direceHandle);
			}
		}
	}
}
