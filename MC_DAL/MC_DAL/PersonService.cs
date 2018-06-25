using MC_DAL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace MC_DAL
{
	public class PersonService
	{
		private McFaceContext mcFace = new McFaceContext();
		public int Add(Person person)
		{
			int result;
			using (this.mcFace = new McFaceContext())
			{
				this.mcFace.Persons.Add(person);
				this.mcFace.SaveChanges();
				result = (int)Convert.ToInt16(person.ID);
			}
			return result;
		}
		public int GetCount()
		{
			int result;
			using (this.mcFace = new McFaceContext())
			{
				result = this.mcFace.Persons.Count<Person>();
			}
			return result;
		}
		public int Update(Person person)
		{
			int result;
			using (this.mcFace = new McFaceContext())
			{
				this.mcFace.Entry<Person>(person).State = EntityState.Modified;
				this.mcFace.SaveChanges();
				result = (int)Convert.ToInt16(person.ID);
			}
			return result;
		}
		public int Clear()
		{
			int result;
			using (this.mcFace = new McFaceContext())
			{
				result = this.mcFace.Database.ExecuteSqlCommand("Delete from Person", new object[0]);
			}
			return result;
		}
		public void Delete(int id)
		{
			using (this.mcFace = new McFaceContext())
			{
				Person entity = this.mcFace.Persons.Find(new object[]
				{
					id
				});
				this.mcFace.Entry<Person>(entity).State = EntityState.Deleted;
				this.mcFace.SaveChanges();
			}
		}
		public void Delete(Person person)
		{
			using (this.mcFace = new McFaceContext())
			{
				this.mcFace.Entry<Person>(person).State = EntityState.Deleted;
				this.mcFace.SaveChanges();
			}
		}
		public Person getTop()
		{
			Person result;
			using (this.mcFace = new McFaceContext())
			{
				result = this.mcFace.Persons.FirstOrDefault<Person>();
			}
			return result;
		}
		public List<Person> GetList()
		{
			List<Person> result;
			using (this.mcFace = new McFaceContext())
			{
				result = this.mcFace.Persons.ToList<Person>();
			}
			return result;
		}
		public List<Person> GetList(long id)
		{
			List<Person> result;
			using (this.mcFace = new McFaceContext())
			{
				result = (
					from p in this.mcFace.Persons
					where p.ID > id
					select p).ToList<Person>();
			}
			return result;
		}
		public List<Person> GetList(string Number, string Name)
		{
			List<Person> result;
			using (this.mcFace = new McFaceContext())
			{
				IQueryable<Person> source = this.mcFace.Persons.AsQueryable<Person>();
				if (!string.Empty.Equals(Name))
				{
					source = 
						from p in source
						where p.Name.Contains(Name)
						select p;
				}
				if (!string.Empty.Equals(Number))
				{
					source = 
						from p in source
						where p.Number.Contains(Number)
						select p;
				}
				result = source.Take(500).ToList<Person>();
			}
			return result;
		}
		public Person GetPerson(int ID)
		{
			Person result;
			using (this.mcFace = new McFaceContext())
			{
				result = this.mcFace.Persons.FirstOrDefault((Person p) => p.ID == (long)ID);
			}
			return result;
		}
	}
}
