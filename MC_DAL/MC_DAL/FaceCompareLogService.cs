using MC_DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MC_DAL
{
	public class FaceCompareLogService
	{
		private McFaceContext _mcFace = new McFaceContext();
		public List<FaceCompareLog> GetList(out int Count, int pagesize, int pageindex)
		{
			List<FaceCompareLog> result;
			using (this._mcFace = new McFaceContext())
			{
				Count = this._mcFace.FaceCompareLogs.Count<FaceCompareLog>();
				result = this._mcFace.FaceCompareLogs.Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList<FaceCompareLog>();
			}
			return result;
		}
		public List<FaceCompareLog> GetList(out int Count, DateTime time, int pagesize, int pageindex)
		{
			List<FaceCompareLog> result;
			using (this._mcFace = new McFaceContext())
			{
				string date = time.ToString("yyyy\\/M\\/d");
				IQueryable<FaceCompareLog> source = this._mcFace.FaceCompareLogs.AsQueryable<FaceCompareLog>();
				source = 
					from p in source
					where p.FaceDetcetDate.Contains(date)
					select p;
				Count = source.Count<FaceCompareLog>();
				result = (
					from p in source
					orderby p.ID descending
					select p).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList<FaceCompareLog>();
			}
			return result;
		}
		public List<FaceCompareLog> GetList(out int Count, DateTime time, string number, int pagesize, int pageindex)
		{
			List<FaceCompareLog> result;
			using (this._mcFace = new McFaceContext())
			{
				string date = time.ToString("yyyy\\/M\\/d");
				IQueryable<FaceCompareLog> source = this._mcFace.FaceCompareLogs.AsQueryable<FaceCompareLog>();
				source = 
					from p in source
					where p.FaceDetcetDate.Contains(date)
					select p;
				source = 
					from p in source
					where p.PersonNumber.Contains(number)
					select p;
				Count = source.Count<FaceCompareLog>();
				result = (
					from p in source
					orderby p.ID descending
					select p).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList<FaceCompareLog>();
			}
			return result;
		}
		public List<FaceCompareLog> GetList(out int Count, DateTime time, string number, string Port, int pagesize, int pageindex)
		{
			List<FaceCompareLog> result;
			using (this._mcFace = new McFaceContext())
			{
				string date = time.ToString("yyyy\\/M\\/d");
				IQueryable<FaceCompareLog> source = this._mcFace.FaceCompareLogs.AsQueryable<FaceCompareLog>();
				if (Port.Equals("全部"))
				{
					source = 
						from p in source
						where p.FaceDetcetDate.Contains(date)
						select p;
				}
				else
				{
					source = 
						from p in source
						where p.FaceDetcetDate.Contains(date)
						where p.tmp1.Equals(Port)
						select p;
				}
				if (!number.Equals(string.Empty))
				{
					source = 
						from p in source
						where p.PersonNumber.Contains(number)
						select p;
				}
				Count = source.Count<FaceCompareLog>();
				result = (
					from p in source
					orderby p.ID descending
					select p).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList<FaceCompareLog>();
			}
			return result;
		}
		public List<FaceCompareLog> GetListByPersonName(DateTime time, string number, int pagesize, int pageindex)
		{
			List<FaceCompareLog> result;
			using (this._mcFace = new McFaceContext())
			{
				result = (
					from p in this._mcFace.FaceCompareLogs
					where p.PersonNumber.Equals(number)
					orderby p.ID descending
					select p).Skip(pagesize * (pageindex - 1)).Take(pagesize).ToList<FaceCompareLog>();
			}
			return result;
		}
		public int Add(FaceCompareLog faceCompareLog)
		{
			int result;
			using (this._mcFace = new McFaceContext())
			{
				this._mcFace.FaceCompareLogs.Add(faceCompareLog);
				this._mcFace.SaveChanges();
				result = (int)Convert.ToInt16(faceCompareLog.ID);
			}
			return result;
		}
		public int GetCount()
		{
			int result;
			using (this._mcFace = new McFaceContext())
			{
				result = this._mcFace.FaceCompareLogs.Count<FaceCompareLog>();
			}
			return result;
		}
		public int Clear()
		{
			int result;
			using (this._mcFace = new McFaceContext())
			{
				result = this._mcFace.Database.ExecuteSqlCommand("DELETE from FaceCompareLog", new object[0]);
			}
			return result;
		}
	}
}
