using log4net;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
namespace FaceCompareBase
{
	public class FaceCompareV2 : IFaceCompare
	{
		private ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly object _obj = new object();
		private static IntPtr _faceEngne = IntPtr.Zero;
		[DllImport("SFaceMatcherSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateFaceEngine")]
		private static extern IntPtr _CreateFaceEngne();
		[DllImport("SFaceMatcherSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ExtractFeature")]
		private static extern int _ExtractFeature(IntPtr engine, byte[] imgRgb24, int width, int height, int pith, ref FaceModelV2 faceModel);
		[DllImport("SFaceMatcherSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Compare2Feature")]
		private static extern float _Compare2Feature(IntPtr engine, byte[] ptFeature1, byte[] ptFeature2);
		[DllImport("SFaceMatcherSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "IdFaceIdentity")]
		private static extern float _Compare2Image(IntPtr engine, string imgFile1, string imgFile2);
		[DllImport("SFaceMatcherSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "FastDetectFaces")]
		private static extern int _FacesDetect(IntPtr engine, byte[] imgRgb24, int width, int height, int widthstep, [MarshalAs(UnmanagedType.LPArray)] [Out] FaceModelV2[] faceRects, int maxCount);
		[DllImport("SFaceMatcherSDK.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Dispose")]
		private static extern int _FreeEngine(IntPtr engine);
		float IFaceCompare.Compare2Feature(byte[] ptFeature1, byte[] ptFeature2)
		{
			float result;
			try
			{
				object obj = FaceCompareV2._obj;
				lock (obj)
				{
					result = FaceCompareV2._Compare2Feature(FaceCompareV2._faceEngne, ptFeature1, ptFeature2);
				}
			}
			catch (Exception)
			{
				result = -1f;
			}
			return result;
		}
		float IFaceCompare.Compare2Image(string imgFile1, string imgFile2)
		{
			object obj = FaceCompareV2._obj;
			float result;
			lock (obj)
			{
				result = FaceCompareV2._Compare2Image(FaceCompareV2._faceEngne, imgFile1, imgFile2);
			}
			return result;
		}
		void IFaceCompare.CreateFaceEngne()
		{
			try
			{
				if (FaceCompareV2._faceEngne == IntPtr.Zero)
				{
					FaceCompareV2._faceEngne = FaceCompareV2._CreateFaceEngne();
				}
			}
			catch (Exception message)
			{
				this._log.Error(message);
			}
		}
		int IFaceCompare.DetectFaces4Image(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel)
		{
			int result;
			try
			{
				object obj = FaceCompareV2._obj;
				lock (obj)
				{
					FaceModelV2[] array = new FaceModelV2[20];
					int num = FaceCompareV2._FacesDetect(FaceCompareV2._faceEngne, bgr24, width, height, widthstep, array, 20);
					if (num > 0)
					{
						for (int i = 0; i < num; i++)
						{
							FaceCompareV2._ExtractFeature(FaceCompareV2._faceEngne, bgr24, width, height, widthstep, ref array[i]);
						}
					}
					faceModel = FaceUnit.FaceModelV2ToFaceModel(array.Take(num).ToArray<FaceModelV2>());
					result = num;
				}
			}
			catch (Exception arg_7A_0)
			{
				throw arg_7A_0;
			}
			return result;
		}
		int IFaceCompare.DetectFaces4Image(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = FaceCompareV2._obj;
				lock (obj)
				{
					FaceModelV2[] array = new FaceModelV2[maxFaceCount];
					int num = FaceCompareV2._FacesDetect(FaceCompareV2._faceEngne, bgr24, width, height, widthstep, array, maxFaceCount);
					if (num > 0)
					{
						for (int i = 0; i < num; i++)
						{
							FaceCompareV2._ExtractFeature(FaceCompareV2._faceEngne, bgr24, width, height, widthstep, ref array[i]);
						}
					}
					faceModel = FaceUnit.FaceModelV2ToFaceModel(array.Take(num).ToArray<FaceModelV2>());
					result = num;
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		int IFaceCompare.DetectFaces4Image_only(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = FaceCompareV2._obj;
				lock (obj)
				{
					FaceModelV2[] array = new FaceModelV2[maxFaceCount];
					int num = FaceCompareV2._FacesDetect(FaceCompareV2._faceEngne, bgr24, width, height, widthstep, array, maxFaceCount);
					faceModel = FaceUnit.FaceModelV2ToFaceModel(array.Take(num).ToArray<FaceModelV2>());
					result = num;
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		int IFaceCompare.ExtractFeature(byte[] bgr24, int width, int height, int widthstep, ref FaceModel faceModel)
		{
			int result;
			try
			{
				object obj = FaceCompareV2._obj;
				lock (obj)
				{
					FaceModelV2 faceModelV = faceModel.ToFaceModelV2();
					int arg_33_0 = FaceCompareV2._ExtractFeature(FaceCompareV2._faceEngne, bgr24, width, height, widthstep, ref faceModelV);
					faceModel = FaceUnit.FaceModelV2ToFaceModel(faceModelV);
					result = arg_33_0;
				}
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		void IFaceCompare.Dispose()
		{
			FaceCompareV2._FreeEngine(FaceCompareV2._faceEngne);
		}
		long IFaceCompare.GetDongerSerial()
		{
			return 0L;
		}
	}
}
