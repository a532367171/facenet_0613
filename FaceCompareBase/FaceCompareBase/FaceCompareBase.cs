using log4net;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
namespace FaceCompareBase
{
	public class FaceCompareBase : IFaceCompare
	{
		private static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly object _obj = new object();
		private static IntPtr _faceEngne = IntPtr.Zero;
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Create")]
		private static extern IntPtr _CreateFaceEngne();
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_ExtractFeatureV2")]
		private static extern int _ExtractFeature(IntPtr engine, byte[] imgRgb24, int width, int height, int pith, ref FaceModelV3 faceModel);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Compare2Feature")]
		private static extern float _Compare2Feature(IntPtr engine, byte[] ptFeature1, byte[] ptFeature2);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Compare2Image")]
		private static extern float _Compare2Image(IntPtr engine, string imgFile1, string imgFile2);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Dispose")]
		private static extern void _Dispose(IntPtr engine);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Process")]
		private static extern int _FacesDetect(IntPtr handle, byte[] image, int width, int height, int pitch, [MarshalAs(UnmanagedType.LPArray)] [Out] FaceModelV3[] results, int faceNum);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_DetectFacesV3")]
		private static extern int _FacesDetectV3(IntPtr engine, byte[] imgRgb24, int width, int height, int widthstep, [MarshalAs(UnmanagedType.LPArray)] [Out] FaceModelV3[] faceModels, int maxCount);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_DetectFacesV2")]
		private static extern int _FacesDetectV2(IntPtr engine, byte[] imgRgb24, int width, int height, int widthstep, [MarshalAs(UnmanagedType.LPArray)] [Out] FaceModelV3[] faceModels, int maxCount);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_GetDongerSerial")]
		private static extern long _GetDongerSerial();
		float IFaceCompare.Compare2Feature(byte[] ptFeature1, byte[] ptFeature2)
		{
			float result;
			try
			{
				object obj = FaceCompareBase._obj;
				lock (obj)
				{
					float num = FaceCompareBase._Compare2Feature(FaceCompareBase._faceEngne, ptFeature1, ptFeature2);
					if (num <= 0f)
					{
						num = 0f;
					}
					if (num >= 1f)
					{
						num = 0.999999f;
					}
					result = num;
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
			object obj = FaceCompareBase._obj;
			float result;
			lock (obj)
			{
				result = FaceCompareBase._Compare2Image(FaceCompareBase._faceEngne, imgFile1, imgFile2);
			}
			return result;
		}
		void IFaceCompare.CreateFaceEngne()
		{
			try
			{
				if (FaceCompareBase._faceEngne == IntPtr.Zero)
				{
					FaceCompareBase._faceEngne = FaceCompareBase._CreateFaceEngne();
				}
			}
			catch (Exception message)
			{
				FaceCompareBase._log.Error(message);
			}
		}
		int IFaceCompare.DetectFaces4Image(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel)
		{
			int result;
			try
			{
				object obj = FaceCompareBase._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[20];
					int num = FaceCompareBase._FacesDetect(FaceCompareBase._faceEngne, bgr24, width, height, widthstep, array, 20);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array.Take(num).ToArray<FaceModelV3>());
					result = num;
				}
			}
			catch (Exception arg_4E_0)
			{
				throw arg_4E_0;
			}
			return result;
		}
		int IFaceCompare.DetectFaces4Image(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = FaceCompareBase._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[maxFaceCount];
					int num = FaceCompareBase._FacesDetect(FaceCompareBase._faceEngne, bgr24, width, height, widthstep, array, maxFaceCount);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array.Take(num).ToArray<FaceModelV3>());
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
				object obj = FaceCompareBase._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[maxFaceCount];
					int num = FaceCompareBase._FacesDetectV3(FaceCompareBase._faceEngne, bgr24, width, height, widthstep, array, maxFaceCount);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array.Take(num).ToArray<FaceModelV3>());
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
				object obj = FaceCompareBase._obj;
				lock (obj)
				{
					FaceModelV3 faceModelV = faceModel.ToFaceModelV3();
					int arg_33_0 = FaceCompareBase._ExtractFeature(FaceCompareBase._faceEngne, bgr24, width, height, widthstep, ref faceModelV);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(faceModelV);
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
			FaceCompareBase._Dispose(FaceCompareBase._faceEngne);
		}
		long IFaceCompare.GetDongerSerial()
		{
			return FaceCompareBase._GetDongerSerial();
		}
	}
}
