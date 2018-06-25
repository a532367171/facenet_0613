using Emgu.CV;
using Emgu.CV.Structure;
using log4net;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
namespace FaceCompareBase
{
	public class FaceCompare
	{
		private static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly object _obj = new object();
		private static IntPtr FaceEngne = IntPtr.Zero;
		private static Stopwatch _watch = new Stopwatch();
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
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Process_AlphaPro")]
		private static extern int _FacesDetect_AlphaPro(IntPtr handle, byte[] image, int width, int height, int pitch, [MarshalAs(UnmanagedType.LPArray)] [Out] FaceModelV3[] results, int faceNum);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Compare2Feature_AlphaPro")]
		private static extern float _Compare2Feature_AlphaPro(IntPtr engine, byte[] ptFeature1, byte[] ptFeature2);
		public static void MC_CreateFaceEngne()
		{
			try
			{
				if (FaceCompare.FaceEngne == IntPtr.Zero)
				{
					FaceCompare.FaceEngne = FaceCompare._CreateFaceEngne();
				}
			}
			catch (Exception message)
			{
				FaceCompare._log.Error(message);
			}
		}
		public static int MC_DetectFaces4Image(Image<Bgr, byte> image, out FaceModel[] faceModel)
		{
			int result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[20];
					int arg_4A_0 = FaceCompare._FacesDetect(FaceCompare.FaceEngne, image.Bytes, image.Width, image.Height, image.MIplImage.WidthStep, array, 20);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array);
					result = arg_4A_0;
				}
			}
			catch (Exception arg_57_0)
			{
				throw arg_57_0;
			}
			return result;
		}
		public static int MC_DetectFaces4Image(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int MaxCount)
		{
			int result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[20];
					int arg_32_0 = FaceCompare._FacesDetect(FaceCompare.FaceEngne, bgr24, width, height, widthstep, array, 20);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array);
					result = arg_32_0;
				}
			}
			catch (Exception arg_3F_0)
			{
				throw arg_3F_0;
			}
			return result;
		}
		public static int MC_DetectFaces4Image(Image<Bgr, byte> image, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[maxFaceCount];
					int arg_48_0 = FaceCompare._FacesDetect(FaceCompare.FaceEngne, image.Bytes, image.Width, image.Height, image.MIplImage.WidthStep, array, maxFaceCount);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array);
					result = arg_48_0;
				}
			}
			catch (Exception arg_55_0)
			{
				throw arg_55_0;
			}
			return result;
		}
		public static int MC_DetectFaces4Image_AlphaPro(Image<Bgr, byte> image, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[maxFaceCount];
					int arg_48_0 = FaceCompare._FacesDetect_AlphaPro(FaceCompare.FaceEngne, image.Bytes, image.Width, image.Height, image.MIplImage.WidthStep, array, maxFaceCount);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array);
					result = arg_48_0;
				}
			}
			catch (Exception arg_55_0)
			{
				throw arg_55_0;
			}
			return result;
		}
		public static int DetectFaces4Image_AlphaPro(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[maxFaceCount];
					int num = FaceCompare._FacesDetect_AlphaPro(FaceCompare.FaceEngne, bgr24, width, height, widthstep, array, maxFaceCount);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array.Take(num).ToArray<FaceModelV3>());
					result = num;
				}
			}
			catch (Exception arg_4D_0)
			{
				throw arg_4D_0;
			}
			return result;
		}
		public static int MC_DetectFaces4Image_only(Image<Bgr, byte> image, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[maxFaceCount];
					int arg_48_0 = FaceCompare._FacesDetectV3(FaceCompare.FaceEngne, image.Bytes, image.Width, image.Height, image.MIplImage.WidthStep, array, maxFaceCount);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(array);
					result = arg_48_0;
				}
			}
			catch (Exception arg_55_0)
			{
				throw arg_55_0;
			}
			return result;
		}
		public static int MC_ExtractFeature(Image<Bgr, byte> image, ref FaceModel faceModel)
		{
			int result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					FaceModelV3 faceModelV = faceModel.ToFaceModelV3();
					int arg_49_0 = FaceCompare._ExtractFeature(FaceCompare.FaceEngne, image.Bytes, image.Width, image.Height, image.MIplImage.Width, ref faceModelV);
					faceModel = FaceUnit.FaceModelV3ToFaceModel(faceModelV);
					result = arg_49_0;
				}
			}
			catch (Exception arg_56_0)
			{
				throw arg_56_0;
			}
			return result;
		}
		public static float MC_Compare2Feature(byte[] ptFeature1, byte[] ptFeature2)
		{
			float result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					result = FaceCompare._Compare2Feature(FaceCompare.FaceEngne, ptFeature1, ptFeature2);
				}
			}
			catch (Exception)
			{
				result = -1f;
			}
			return result;
		}
		public static float MC_Compare2Feature_AlphaPro(byte[] ptFeature1, byte[] ptFeature2)
		{
			float result;
			try
			{
				object obj = FaceCompare._obj;
				lock (obj)
				{
					result = FaceCompare._Compare2Feature_AlphaPro(FaceCompare.FaceEngne, ptFeature1, ptFeature2);
				}
			}
			catch (Exception)
			{
				result = -1f;
			}
			return result;
		}
		public static float MC_Compare2Image(string imgFile1, string imgFile2)
		{
			object obj = FaceCompare._obj;
			float result;
			lock (obj)
			{
				result = FaceCompare._Compare2Image(FaceCompare.FaceEngne, imgFile1, imgFile2);
			}
			return result;
		}
		public static void Dispose()
		{
			if (FaceCompare.FaceEngne != IntPtr.Zero)
			{
				FaceCompare._Dispose(FaceCompare.FaceEngne);
			}
		}
	}
}
