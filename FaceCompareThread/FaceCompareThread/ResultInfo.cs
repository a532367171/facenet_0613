using FaceCompareBase;
using System;
namespace FaceCompareThread
{
	[Serializable]
	public class ResultInfo
	{
		public FaceTemplate FaceTemplate
		{
			get;
			set;
		}
		public byte[] FaceImage
		{
			get;
			set;
		}
		public float Score
		{
			get;
			set;
		}
		public FaceModel FaceModel
		{
			get;
			set;
		}
		public ResultInfo()
		{
			this.FaceTemplate = new FaceTemplate();
		}
	}
}
