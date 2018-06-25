using System;
using System.Runtime.InteropServices;
namespace FaceCompareBase
{
	public struct FaceModelV4
	{
		public FaceModelRectV4 FaceRect;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
		public byte[] Feature;
	}
}
