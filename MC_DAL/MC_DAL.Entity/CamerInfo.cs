using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MC_DAL.Entity
{
	[System.ComponentModel.DataAnnotations.Schema.Table("CamerInfo")]
	public class CamerInfo
	{
		[Key]
		public long ID
		{
			get;
			set;
		}
		public string Channel
		{
			get;
			set;
		}
		public string CamerType
		{
			get;
			set;
		}
		public string CamerAddress
		{
			get;
			set;
		}
		public string CamerPort
		{
			get;
			set;
		}
		public string CamerUser
		{
			get;
			set;
		}
		public string CamerPassword
		{
			get;
			set;
		}
		public long CamerHeight
		{
			get;
			set;
		}
		public long CamerWeight
		{
			get;
			set;
		}
		public long IsTure
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
