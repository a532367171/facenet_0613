using System;
using System.Runtime.InteropServices;
namespace FaceCompareBase
{
	public struct FaceModelV3
	{
		public FaceModelRectV3 FaceRect;
		public FacePointInt LeftEyeFacePointInt;
		public FacePointInt RightEyeFacePointInt;
		public FacePointInt Nose;
		public FacePointInt LeftMouthFacePointInt;
		public FacePointInt RightMouthFacePointInt;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024, ArraySubType = UnmanagedType.I1)]
		public byte[] Feature;
	}
}
