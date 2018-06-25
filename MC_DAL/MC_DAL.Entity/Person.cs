using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MC_DAL.Entity
{
	[System.ComponentModel.DataAnnotations.Schema.Table("Person")]
	public class Person
	{
		[Key]
		public long ID
		{
			get;
			set;
		}
		public string Number
		{
			get;
			set;
		}
		public string Feature
		{
			get;
			set;
		}
		public string ImageLoaction
		{
			get;
			set;
		}
		public string Image
		{
			get;
			set;
		}
		public long Sex
		{
			get;
			set;
		}
		public long Age
		{
			get;
			set;
		}
		public string Remark
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
		public string Name
		{
			get;
			set;
		}
	}
}
