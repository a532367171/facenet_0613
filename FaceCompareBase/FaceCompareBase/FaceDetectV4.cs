using System;
using System.Runtime.InteropServices;
namespace FaceCompareBase
{
	public class FaceDetectV4
	{
		private IntPtr _faceEngne = IntPtr.Zero;
		private readonly object _obj = new object();
		[DllImport("WisFaceEngineWrapV4.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Create")]
		private static extern IntPtr _CreateFaceEngne(int tag);
		[DllImport("WisFaceEngineWrapV4.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Create")]
		private static extern IntPtr _CreateFaceEngne();
		[DllImport("WisFaceEngineWrapV4.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_ExtractFeature")]
		private static extern int _ExtractFeature(IntPtr engine, byte[] imgRgb24, int width, int height, int pith, FaceModelRectV4 facerRect, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] feature);
		[DllImport("WisFaceEngineWrapV4.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Compare2Feature")]
		private static extern float _Compare2Feature(IntPtr engine, byte[] ptFeature1, byte[] ptFeature2);
		[DllImport("WisFaceEngineWrapV4.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Compare2Image")]
		private static extern float _Compare2Image(IntPtr engine, string imgFile1, string imgFile2);
		[DllImport("WisFaceEngineWrapV4.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Dispose")]
		private static extern void _Dispose(IntPtr engine);
		[DllImport("WisFaceEngineWrapV4.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_DetectFaces")]
		private static extern int _FacesDetects(IntPtr engine, byte[] imgRgb24, int width, int height, int widthstep, [MarshalAs(UnmanagedType.LPArray)] [Out] FaceModelRectV4[] faceRects, int maxCount);
		public void CreateFaceEngne()
		{
			object obj = this._obj;
			lock (obj)
			{
				if (this._faceEngne == IntPtr.Zero)
				{
					if (IntPtr.Size == 4)
					{
						this._faceEngne = FaceDetectV4._CreateFaceEngne();
					}
					else
					{
						if (IntPtr.Size == 8)
						{
							this._faceEngne = FaceDetectV4._CreateFaceEngne(0);
						}
					}
					Console.WriteLine(this._faceEngne);
				}
			}
		}
		public virtual void CreateDetectFaceEngine()
		{
			object obj = this._obj;
			lock (obj)
			{
				if (this._faceEngne == IntPtr.Zero)
				{
					if (IntPtr.Size == 4)
					{
						this._faceEngne = FaceDetectV4._CreateFaceEngne();
					}
					else
					{
						if (IntPtr.Size == 8)
						{
							this._faceEngne = FaceDetectV4._CreateFaceEngne(1);
						}
					}
					Console.WriteLine(this._faceEngne);
				}
			}
		}
		public virtual int DetectFaces4Image_only(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = this._obj;
				lock (obj)
				{
					FaceModelRectV4[] array = new FaceModelRectV4[10];
					int num = FaceDetectV4._FacesDetects(this._faceEngne, bgr24, width, height, widthstep, array, 10);
					if (num > 0)
					{
						FaceModelV4[] array2 = new FaceModelV4[num];
						for (int i = 0; i < num; i++)
						{
							array2[i].FaceRect = array[i];
						}
						faceModel = FaceUnit.FaceModelV4ToFaceModel(array2);
					}
					else
					{
						faceModel = new FaceModel[0];
					}
					result = num;
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		public void Dispose()
		{
			FaceDetectV4._Dispose(this._faceEngne);
		}
	}
}
