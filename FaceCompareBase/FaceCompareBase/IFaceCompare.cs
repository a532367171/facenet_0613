using System;
namespace FaceCompareBase
{
	public interface IFaceCompare
	{
		void CreateFaceEngne();
		int DetectFaces4Image(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel);
		int DetectFaces4Image(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int maxFaceCount);
		int DetectFaces4Image_only(byte[] bgr24, int width, int height, int widthstep, out FaceModel[] faceModel, int maxFaceCount);
		int ExtractFeature(byte[] bgr24, int width, int height, int widthstep, ref FaceModel faceModel);
		float Compare2Feature(byte[] ptFeature1, byte[] ptFeature2);
		float Compare2Image(string imgFile1, string imgFile2);
		long GetDongerSerial();
		void Dispose();
	}
}
