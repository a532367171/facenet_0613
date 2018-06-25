using System;
namespace FaceCompareBase
{
	[Serializable]
	public class FaceModelRect
	{
		public int Left
		{
			get;
			set;
		}
		public int Top
		{
			get;
			set;
		}
		public int Right
		{
			get;
			set;
		}
		public int Bottom
		{
			get;
			set;
		}
		public float fConf
		{
			get;
			set;
		}
		public float fRotAngle
		{
			get;
			set;
		}
		public float Yaw
		{
			get;
			set;
		}
		public float Roll
		{
			get;
			set;
		}
		public float Pitch
		{
			get;
			set;
		}
		public FaceModelRectV3 ToFaceModelRectV3()
		{
			return new FaceModelRectV3
			{
				Bottom = this.Bottom,
				Left = this.Left,
				Top = this.Top,
				Right = this.Right
			};
		}
		public FaceModelRectV4 ToFaceModelRectV4()
		{
			return new FaceModelRectV4
			{
				Bottom = this.Bottom,
				Left = this.Left,
				Top = this.Top,
				Right = this.Right,
				fConf = this.fConf,
				fRotAngle = this.fRotAngle
			};
		}
	}
}
