using System;
namespace FaceCompareBase
{
	[Serializable]
	public class FaceModelPoint
	{
		public int X
		{
			get;
			set;
		}
		public int Y
		{
			get;
			set;
		}
		public FacePointInt ToFacePointInt()
		{
			return new FacePointInt
			{
				X = this.X,
				Y = this.Y
			};
		}
		public FacePointFloat ToFacePointFloat()
		{
			return new FacePointFloat
			{
				X = (float)this.X,
				Y = (float)this.Y
			};
		}
	}
}
