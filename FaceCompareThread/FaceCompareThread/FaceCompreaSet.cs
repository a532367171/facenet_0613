using FaceCompareBase;
using System;
using System.Collections.Generic;
namespace FaceCompareThread
{
	public class FaceCompreaSet
	{
		public float Threshold = 0.7f;
		public List<FaceTemplate> FaceTemplates = new List<FaceTemplate>();
		public int CompareSuccessCount = 1;
		public int Between2Eyes = 35;
		public bool IsMaxFace;
		public CameraInfor CameraInfor;
		public bool IsShowFaceRectangle = true;
		public int FrameRate = 1;
		public float FaceConf = 0.7f;
		public FaceCompareType FaceCompareType = FaceCompareType.FaceComparePro;
		public bool HardwareAcceleration = true;
	}
}
