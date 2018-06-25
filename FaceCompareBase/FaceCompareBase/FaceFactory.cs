using System;
namespace FaceCompareBase
{
	public static class FaceFactory
	{
		public static IFaceCompare Create(FaceCompareType faceCompareType)
		{
			IFaceCompare result = null;
			switch (faceCompareType)
			{
			case FaceCompareType.FaceCompareBase:
				result = new FaceCompareBase();
				break;
			case FaceCompareType.FaceComparePro:
				result = new FaceComparePro();
				break;
			case FaceCompareType.FaceCompareV4:
				result = new FaceCompareV4();
				break;
			case FaceCompareType.FaceCompareV2:
				result = new FaceCompareV2();
				break;
			}
			return result;
		}
	}
}
