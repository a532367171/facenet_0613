using MC_DAL.Entity;
using System;
using System.Data.Entity;
namespace MC_DAL
{
	public class McFaceContext : DbContext
	{
		public DbSet<Person> Persons
		{
			get;
			set;
		}
		public DbSet<CamerInfo> CamerInfos
		{
			get;
			set;
		}
		public DbSet<FaceCompareLog> FaceCompareLogs
		{
			get;
			set;
		}
	}
}
