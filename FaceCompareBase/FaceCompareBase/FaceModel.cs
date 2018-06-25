using System;
namespace FaceCompareBase
{
	[Serializable]
	public class FaceModel
	{
		public FaceModelRect FaceRect
		{
			get;
			set;
		}
		public FaceModelPoint LeftEyeFacePoint
		{
			get;
			set;
		}
		public FaceModelPoint RightEyeFacePoint
		{
			get;
			set;
		}
		public FaceModelPoint Nose
		{
			get;
			set;
		}
		public FaceModelPoint LeftMouthFacePoint
		{
			get;
			set;
		}
		public FaceModelPoint RightMouthFacePoint
		{
			get;
			set;
		}
		public byte[] Feature
		{
			get;
			set;
		}
		public FaceModel Clone()
		{
			return new FaceModel
			{
				FaceRect = this.FaceRect,
				LeftEyeFacePoint = this.LeftEyeFacePoint,
				RightEyeFacePoint = this.RightEyeFacePoint,
				Nose = this.Nose,
				RightMouthFacePoint = this.RightMouthFacePoint,
				LeftMouthFacePoint = this.LeftMouthFacePoint,
				Feature = this.Feature
			};
		}
		public FaceModelV2 ToFaceModelV2()
		{
			return new FaceModelV2
			{
				FaceRect = this.FaceRect.ToFaceModelRectV3(),
				Feature = this.Feature,
				LeftEyeFacePointInt = this.LeftEyeFacePoint.ToFacePointInt(),
				LeftMouthFacePointInt = this.LeftMouthFacePoint.ToFacePointInt(),
				Nose = this.Nose.ToFacePointInt(),
				RightEyeFacePointInt = this.RightEyeFacePoint.ToFacePointInt(),
				RightMouthFacePointInt = this.RightMouthFacePoint.ToFacePointInt(),
				Conf = this.FaceRect.fConf,
				Angle = this.FaceRect.Roll
			};
		}
		public FaceModelV3 ToFaceModelV3()
		{
			FaceModelV3 faceModelV = new FaceModelV3
			{
				FaceRect = this.FaceRect.ToFaceModelRectV3(),
				Feature = this.Feature,
				LeftEyeFacePointInt = this.LeftEyeFacePoint.ToFacePointInt(),
				LeftMouthFacePointInt = this.LeftMouthFacePoint.ToFacePointInt(),
				Nose = this.Nose.ToFacePointInt(),
				RightEyeFacePointInt = this.RightEyeFacePoint.ToFacePointInt(),
				RightMouthFacePointInt = this.RightMouthFacePoint.ToFacePointInt()
			};
			if (faceModelV.Feature == null)
			{
				faceModelV.Feature = new byte[1024];
			}
			return faceModelV;
		}
		public static FaceModelV4 ToFaceModelV4(FaceModel faceModel)
		{
			return new FaceModelV4
			{
				FaceRect = faceModel.FaceRect.ToFaceModelRectV4(),
				Feature = faceModel.Feature
			};
		}
	}
}
