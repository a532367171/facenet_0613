using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WisFaceBase;
namespace FaceCompareBase
{
	public static class FaceUnit
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class class_c 
		{
			public static readonly FaceUnit.class_c class_c_9 = new FaceUnit.class_c();
			public static Func<FaceModel, int> class_c_9__0_0;
			public static Func<FaceModel, int> class_c_9__2_0;
			internal int _FaceModelV2ToFaceModel_b__0_0(FaceModel p)
			{
				return p.FaceRect.Right - p.FaceRect.Left;
			}
			internal int _FaceModelV3ToFaceModel_b__2_0(FaceModel p)
			{
				return p.FaceRect.Right - p.FaceRect.Left;
			}
		}
		public static FaceModel[] FaceModelV2ToFaceModel(FaceModelV2[] faceModelV2)
		{
			FaceModel[] array = new FaceModel[faceModelV2.Length];
			for (int i = 0; i < faceModelV2.Length; i++)
			{
				array[i] = FaceUnit.FaceModelV2ToFaceModel(faceModelV2[i]);
			}
			if (array.Length > 1)
			{
				IEnumerable<FaceModel> arg_4C_0 = array;
				Func<FaceModel, int> arg_4C_1;
				if ((arg_4C_1 = FaceUnit.class_c.class_c_9__0_0) == null)
				{
					arg_4C_1 = (FaceUnit.class_c.class_c_9__0_0 = new Func<FaceModel, int>(FaceUnit.class_c.class_c_9._FaceModelV2ToFaceModel_b__0_0));
				}
				array = arg_4C_0.OrderByDescending(arg_4C_1).ToArray<FaceModel>();
			}
			return array;
		}
		public static FaceModel FaceModelV2ToFaceModel(FaceModelV2 faceModelV2)
		{
            //return new FaceModel
            //{
            //    FaceRect = FaceUnit.FaceModelRectV3ToFaceModelRect(faceModelV2.FaceRect),
            //    Feature = faceModelV2.Feature,
            //    LeftEyeFacePoint = FaceUnit.ToFaceModelPoint(faceModelV2.LeftEyeFacePointInt),
            //    LeftMouthFacePoint = FaceUnit.ToFaceModelPoint(faceModelV2.LeftMouthFacePointInt),
            //    Nose = FaceUnit.ToFaceModelPoint(faceModelV2.Nose),
            //    RightEyeFacePoint = FaceUnit.ToFaceModelPoint(faceModelV2.RightEyeFacePointInt),
            //    RightMouthFacePoint = FaceUnit.ToFaceModelPoint(faceModelV2.RightMouthFacePointInt),
            //    FaceRect.fConf = faceModelV2.Conf,
            //    FaceRect.fRotAngle = faceModelV2.Angle
            //    //FaceRect =
            //    //{
            //    //    fConf = faceModelV2.Conf,
            //    //    fRotAngle = faceModelV2.Angle
            //    //}

            //    //FaceRect.fConf = faceModelV2.Conf;
            //    //FaceRect.fRotAngle = faceModelV2.Angle;
            //    //    =
            //    //{
            //    //    fConf = faceModelV2.Conf,
            //    //    fRotAngle = faceModelV2.Angle
            //    //}
            //};
            FaceModel  x= new FaceModel
            {
                FaceRect = FaceUnit.FaceModelRectV3ToFaceModelRect(faceModelV2.FaceRect),
                Feature = faceModelV2.Feature,
                LeftEyeFacePoint = FaceUnit.ToFaceModelPoint(faceModelV2.LeftEyeFacePointInt),
                LeftMouthFacePoint = FaceUnit.ToFaceModelPoint(faceModelV2.LeftMouthFacePointInt),
                Nose = FaceUnit.ToFaceModelPoint(faceModelV2.Nose),
                RightEyeFacePoint = FaceUnit.ToFaceModelPoint(faceModelV2.RightEyeFacePointInt),
                RightMouthFacePoint = FaceUnit.ToFaceModelPoint(faceModelV2.RightMouthFacePointInt)
                //FaceRect.fConf = faceModelV2.Conf,
                //FaceRect.fRotAngle = faceModelV2.Angle
                //FaceRect =
                //{
                //    fConf = faceModelV2.Conf,
                //    fRotAngle = faceModelV2.Angle
                //}

                //FaceRect.fConf = faceModelV2.Conf;
                //FaceRect.fRotAngle = faceModelV2.Angle;
                //    =
                //{
                //    fConf = faceModelV2.Conf,
                //    fRotAngle = faceModelV2.Angle
                //}
            };
            x.FaceRect.fConf = faceModelV2.Conf;
            x.FaceRect.fRotAngle = faceModelV2.Angle;
            return x;

        }
		public static FaceModel[] FaceModelV3ToFaceModel(FaceModelV3[] faceModelV3)
		{
			FaceModel[] array = new FaceModel[faceModelV3.Length];
			for (int i = 0; i < faceModelV3.Length; i++)
			{
				array[i] = FaceUnit.FaceModelV3ToFaceModel(faceModelV3[i]);
			}
			if (array.Length > 1)
			{
				IEnumerable<FaceModel> arg_4C_0 = array;
				Func<FaceModel, int> arg_4C_1;
				if ((arg_4C_1 = FaceUnit.class_c.class_c_9__2_0) == null)
				{
					arg_4C_1 = (FaceUnit.class_c.class_c_9__2_0 = new Func<FaceModel, int>(FaceUnit.class_c.class_c_9._FaceModelV3ToFaceModel_b__2_0));
				}
				array = arg_4C_0.OrderByDescending(arg_4C_1).ToArray<FaceModel>();
			}
			return array;
		}
		public static FaceModel FaceModelV3ToFaceModel(FaceModelV3 faceModelV3)
		{
			return new FaceModel
			{
				FaceRect = FaceUnit.FaceModelRectV3ToFaceModelRect(faceModelV3.FaceRect),
				Feature = faceModelV3.Feature,
				LeftEyeFacePoint = FaceUnit.ToFaceModelPoint(faceModelV3.LeftEyeFacePointInt),
				LeftMouthFacePoint = FaceUnit.ToFaceModelPoint(faceModelV3.LeftMouthFacePointInt),
				Nose = FaceUnit.ToFaceModelPoint(faceModelV3.Nose),
				RightEyeFacePoint = FaceUnit.ToFaceModelPoint(faceModelV3.RightEyeFacePointInt),
				RightMouthFacePoint = FaceUnit.ToFaceModelPoint(faceModelV3.RightMouthFacePointInt)
			};
		}
		public static FaceModelPoint ToFaceModelPoint(FacePointInt facePointInt)
		{
			return new FaceModelPoint
			{
				X = facePointInt.X,
				Y = facePointInt.Y
			};
		}
		public static FaceModelRect FaceModelRectV3ToFaceModelRect(FaceModelRectV3 faceModelRectV3)
		{
			return new FaceModelRect
			{
				Left = faceModelRectV3.Left,
				Top = faceModelRectV3.Top,
				Right = faceModelRectV3.Right,
				Bottom = faceModelRectV3.Bottom
			};
		}
		public static FaceModel[] FaceModelV4ToFaceModel(FaceModelV4[] faceModelV4)
		{
			FaceModel[] array = new FaceModel[faceModelV4.Length];
			for (int i = 0; i < faceModelV4.Length; i++)
			{
				array[i] = FaceUnit.FaceModelV4ToFaceModel(faceModelV4[i]);
			}
			return array;
		}
		public static FaceModel FaceModelV4ToFaceModel(FaceModelV4 faceModelV4)
		{
			return new FaceModel
			{
				FaceRect = FaceUnit.FaceModelRectV3ToFaceModelRect(faceModelV4.FaceRect),
				Feature = faceModelV4.Feature
			};
		}
		public static FaceModelRect FaceModelRectV3ToFaceModelRect(FaceModelRectV4 faceModelRectV4)
		{
			return new FaceModelRect
			{
				Bottom = faceModelRectV4.Bottom,
				Left = faceModelRectV4.Left,
				Top = faceModelRectV4.Top,
				Right = faceModelRectV4.Right,
				fConf = faceModelRectV4.fConf,
				fRotAngle = faceModelRectV4.fRotAngle
			};
		}
		public static FaceModelRectV3 WisRectToFaceModelRectV3(WisRect wisRect)
		{
			return new FaceModelRectV3
			{
				Left = wisRect.Left,
				Top = wisRect.Top,
				Right = wisRect.Right,
				Bottom = wisRect.Bottom
			};
		}
		public static FaceModelRect WisRectToFaceModelRect(WisFace wisFace)
		{
			return new FaceModelRect
			{
				Left = wisFace.rect.Left,
				Top = wisFace.rect.Top,
				Right = wisFace.rect.Right,
				Bottom = wisFace.rect.Bottom,
				fConf = wisFace.conf,
				fRotAngle = 0f,
				Pitch = wisFace.pitch,
				Roll = wisFace.roll,
				Yaw = wisFace.yaw
			};
		}
		public static FaceModel WisFaceToFaceModel(WisFace wisFace)
		{
			//return new FaceModel
			//{
			//	FaceRect = FaceUnit.WisRectToFaceModelRect(wisFace),
			//	LeftEyeFacePoint = FaceUnit.WisPointToFaceModelPoint(wisFace.ptLeftEye),
			//	RightEyeFacePoint = FaceUnit.WisPointToFaceModelPoint(wisFace.ptRightEye),
			//	LeftMouthFacePoint = FaceUnit.WisPointToFaceModelPoint(wisFace.ptMouthLeft),
			//	RightMouthFacePoint = FaceUnit.WisPointToFaceModelPoint(wisFace.ptMouthRight),
			//	Nose = FaceUnit.WisPointToFaceModelPoint(wisFace.ptNose),
			//	FaceRect = 
			//	{
			//		fConf = wisFace.conf,
			//		Roll = wisFace.roll,
			//		Yaw = wisFace.yaw,
			//		Pitch = wisFace.pitch
			//	}
			//};

            FaceModel y= new FaceModel
            {
                FaceRect = FaceUnit.WisRectToFaceModelRect(wisFace),
                LeftEyeFacePoint = FaceUnit.WisPointToFaceModelPoint(wisFace.ptLeftEye),
                RightEyeFacePoint = FaceUnit.WisPointToFaceModelPoint(wisFace.ptRightEye),
                LeftMouthFacePoint = FaceUnit.WisPointToFaceModelPoint(wisFace.ptMouthLeft),
                RightMouthFacePoint = FaceUnit.WisPointToFaceModelPoint(wisFace.ptMouthRight),
                Nose = FaceUnit.WisPointToFaceModelPoint(wisFace.ptNose), 
            };
            y.FaceRect.fConf = wisFace.conf;
            y.FaceRect.Roll = wisFace.roll;
            y.FaceRect.Yaw = wisFace.yaw;
            y.FaceRect.Pitch = wisFace.pitch;
            return y;


        }
		public static FaceModelPoint WisPointToFaceModelPoint(WisPoint wisPoint)
		{
			return new FaceModelPoint
			{
				X = wisPoint.X,
				Y = wisPoint.Y
			};
		}
	}
}
