using System;
using System.Runtime.InteropServices;
namespace WisFaceBase
{
	public class WinFaceCore
	{
		private readonly object _obj = new object();
		private IntPtr _faceEngine = IntPtr.Zero;
		[DllImport("WisFaceSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WisFaceSDK_Create")]
		private static extern IntPtr Wis_Create(string dir);
		[DllImport("WisFaceSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WisFaceSDK_DetectFaces")]
		private static extern int Wis_DetectFaces(IntPtr engine, byte[] imgRgb24, int width, int height, int widthstep, [MarshalAs(UnmanagedType.LPArray)] [Out] WisFace[] winFaces, int maxCount);
		[DllImport("WisFaceSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WisFaceSDK_Destroy")]
		private static extern void Wis_Destroy(IntPtr engine);
		public void CreateFaceEngine()
		{
			if (this._faceEngine == IntPtr.Zero)
			{
				this._faceEngine = WinFaceCore.Wis_Create("./");
				Console.WriteLine(string.Format("Create FaceDetect Engine Scuess {0}", this._faceEngine));
			}
		}
		public int DetectFaces(byte[] imgRgb24, int width, int height, int widthstep, out WisFace[] winFaces, int maxCount)
		{
			int result;
			try
			{
				object obj = this._obj;
				lock (obj)
				{
					winFaces = new WisFace[maxCount];
					result = WinFaceCore.Wis_DetectFaces(this._faceEngine, imgRgb24, width, height, widthstep, winFaces, maxCount);
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		public void Destroy(IntPtr engine)
		{
			WinFaceCore.Wis_Destroy(engine);
		}
	}
}
