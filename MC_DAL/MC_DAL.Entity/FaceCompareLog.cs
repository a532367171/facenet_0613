using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MC_DAL.Entity
{
	[System.ComponentModel.DataAnnotations.Schema.Table("FaceCompareLog")]
	public class FaceCompareLog
	{
		[Key]
		public long ID
		{
			get;
			set;
		}
		public long PersonID
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
		public string FaceTempateImage
		{
			get;
			set;
		}
		public string FaceDetcetImage
		{
			get;
			set;
		}
		public string FaceDetcetDate
		{
			get;
			set;
		}
		public string Similarity
		{
			get;
			set;
		}
		public string tmp1
		{
			get;
			set;
		}
		public string tmp2
		{
			get;
			set;
		}
		public string tmp3
		{
			get;
			set;
		}
	}
}
