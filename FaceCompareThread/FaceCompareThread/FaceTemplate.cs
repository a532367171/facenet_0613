using System;
namespace FaceCompareThread
{
	[Serializable]
	public class FaceTemplate
	{
		public string PersonId
		{
			get;
			set;
		}
		public string PersonName
		{
			get;
			set;
		}
		public string PersonNumber
		{
			get;
			set;
		}
		public byte[] FaceFeature
		{
			get;
			set;
		}
		public string ImageLocation
		{
			get;
			set;
		}
		public PersonType PersonType
		{
			get;
			set;
		}
	}
}
