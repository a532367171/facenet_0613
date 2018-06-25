using System;
namespace FaceCompareThread
{
	public class CameraInfor
	{
		public int Index
		{
			get;
			set;
		}
		public CameraType cameraType
		{
			get;
			set;
		}
		public string IP
		{
			get;
			set;
		}
		public ushort DevicePort
		{
			get;
			set;
		}
		public string UserName
		{
			get;
			set;
		}
		public string PassWord
		{
			get;
			set;
		}
		public IntPtr ShowHwnd
		{
			get;
			set;
		}
		public int Height
		{
			get;
			set;
		}
		public int Width
		{
			get;
			set;
		}
	}
}
