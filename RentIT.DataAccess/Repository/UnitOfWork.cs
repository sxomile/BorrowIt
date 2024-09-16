using RentIT.DataAccess.Data;
using RentIT.DataAccess.Repository.IRepository;
using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		ApplicationDbContext _db;
		public IItemRepository Item { get; private set; }
		public IUserRepository User { get; private set; }
		public IOrderRepository Order { get; private set; }
		public ICountryRepository Country { get; private set; }
		public ICityRepository City { get; private set; }
		public IReportRepository Report { get; private set; }
		public IItemCategoryRepository ItemCategory { get; private set; }
		public IItemItemCategoryRepository ItemItemCategory { get; private set; }
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			Item = new ItemRepository(_db);
			User = new UserRepository(_db);
			Order = new OrderRepository(_db);
			Country = new CountryRepository(_db);
			City = new CityRepository(_db);
			Report = new ReportRepository(_db);
			ItemCategory = new ItemCategoryRepository(_db);
			ItemItemCategory = new ItemItemCategoryRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
