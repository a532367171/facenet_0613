using Emgu.CV;
using Emgu.CV.Structure;
using log4net;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
namespace FaceCompareBase
{
	public class FaceComparePro : IFaceCompare
	{
		private const string PATH = "WisFaceEngineWrap.dll";
		private ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private static IntPtr FaceEngne = IntPtr.Zero;
		private static readonly object _obj = new object();
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Create")]
		private static extern IntPtr _CreateFaceEngne();
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Dispose")]
		private static extern void _Dispose(IntPtr engine);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Process_AlphaPro")]
		private static extern int _FacesDetect_AlphaPro(IntPtr handle, byte[] image, int width, int height, int pitch, [MarshalAs(UnmanagedType.LPArray)] [Out] FaceModelV3[] results, int faceNum);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_Compare2Feature_AlphaPro")]
		private static extern float _Compare2Feature_AlphaPro(IntPtr engine, byte[] ptFeature1, byte[] ptFeature2);
		[DllImport("WisFaceEngineWrap.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "qs_Wis_GetDongerSerial")]
		private static extern long _GetDongerSerial();
		public float Compare2Feature(byte[] ptFeature1, byte[] ptFeature2)
		{
			float result;
			try
			{
				object obj = FaceComparePro._obj;
				lock (obj)
				{
					result = FaceComparePro._Compare2Feature_AlphaPro(FaceComparePro.FaceEngne, ptFeature1, ptFeature2);
				}
			}
			catch (Exception)
			{
				result = -1f;
			}
			return result;
		}
		public float Compare2Image(string imgFile1, string imgFile2)
		{
			float result;
			try
			{
				Image<Bgr, byte> image = new Image<Bgr, byte>(imgFile1);
				Image<Bgr, byte> image2 = new Image<Bgr, byte>(imgFile2);
				FaceModel[] array;
				if (this.DetectFaces4Image(image.Bytes, image.Width, image.Height, image.MIplImage.WidthStep, out array) <= 0)
				{
					result = 0f;
				}
				else
				{
					FaceModel[] array2;
					if (this.DetectFaces4Image(image2.Bytes, image2.Width, image2.Height, image2.MIplImage.WidthStep, out array2) <= 0)
					{
						result = 0f;
					}
					else
					{
						image.Dispose();
						image2.Dispose();
						result = this.Compare2Feature(array[0].Feature, array2[0].Feature);
					}
				}
			}
			catch (Exception arg_96_0)
			{
				throw arg_96_0;
			}
			return result;
		}
		public void CreateFaceEngne()
		{
			try
			{
				if (FaceComparePro.FaceEngne == IntPtr.Zero)
				{
					FaceComparePro.FaceEngne = FaceComparePro._CreateFaceEngne();
					Console.WriteLine(FaceComparePro.FaceEngne);
				}
			}
			catch (Exception message)
			{
				this._log.Error(message);
			}
		}
		public int DetectFaces4Image(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel)
		{
			int result;
			try
			{
				object obj = FaceComparePro._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[10];
					int num = FaceComparePro._FacesDetect_AlphaPro(FaceComparePro.FaceEngne, bgr24, width, height, widthstep, array, 10);
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
				object obj = FaceComparePro._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[maxFaceCount];
					int num = FaceComparePro._FacesDetect_AlphaPro(FaceComparePro.FaceEngne, bgr24, width, height, widthstep, array, maxFaceCount);
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
		int IFaceCompare.DetectFaces4Image_only(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int maxFaceCount)
		{
			int result;
			try
			{
				object obj = FaceComparePro._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[maxFaceCount];
					int num = FaceComparePro._FacesDetect_AlphaPro(FaceComparePro.FaceEngne, bgr24, width, height, widthstep, array, maxFaceCount);
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
		int IFaceCompare.ExtractFeature(byte[] bgr24, int width, int height, int widthstep, ref FaceModel faceModel)
		{
			int result;
			try
			{
				object obj = FaceComparePro._obj;
				lock (obj)
				{
					FaceModelV3[] array = new FaceModelV3[1];
					int num = FaceComparePro._FacesDetect_AlphaPro(FaceComparePro.FaceEngne, bgr24, width, height, widthstep, array, 1);
					if (num > 0)
					{
						faceModel = FaceUnit.FaceModelV3ToFaceModel(array.FirstOrDefault<FaceModelV3>());
					}
					else
					{
						faceModel = new FaceModel();
						num = -1;
					}
					result = num;
				}
			}
			catch (Exception arg_56_0)
			{
				throw arg_56_0;
			}
			return result;
		}
		public void Dispose()
		{
			FaceComparePro._Dispose(FaceComparePro.FaceEngne);
		}
		public long GetDongerSerial()
		{
			return FaceComparePro._GetDongerSerial();
		}
	}
}
