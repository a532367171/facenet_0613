using MC_DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace MC_DAL
{
	public class CamerInfoService
	{
		private McFaceContext mcFace = new McFaceContext();
		public List<CamerInfo> GetList()
		{
			List<CamerInfo> result;
			using (this.mcFace = new McFaceContext())
			{
				result = this.mcFace.CamerInfos.ToList<CamerInfo>();
			}
			return result;
		}
		public long Update(CamerInfo camerInfo)
		{
			long iD;
			using (this.mcFace = new McFaceContext())
			{
				this.mcFace.Entry<CamerInfo>(camerInfo).State = EntityState.Modified;
				this.mcFace.SaveChanges();
				iD = camerInfo.ID;
			}
			return iD;
		}
		public long add(CamerInfo camerInfo)
		{
			long iD;
			using (this.mcFace = new McFaceContext())
			{
				this.mcFace.CamerInfos.Add(camerInfo);
				this.mcFace.SaveChanges();
				iD = camerInfo.ID;
			}
			return iD;
		}
	}
}
