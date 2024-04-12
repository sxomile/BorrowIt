using RentIT.DataAccess.Data;
using RentIT.DataAccess.Repository.IRepository;
using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.DataAccess.Repository
{
	public class ItemRepository : Repository<Item>, IItemRepository
	{
		private readonly ApplicationDbContext _db;
		public ItemRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public void Update(Item item)
		{
			var itemDB = _db.Items.FirstOrDefault(i => i.Id == item.Id);
			if(itemDB != null)
			{
				itemDB.Name = item.Name;
				itemDB.Description = item.Description;
				itemDB.RentTime = item.RentTime;
				if(item.ImageUrl != string.Empty)
				{
					itemDB.ImageUrl = item.ImageUrl;
				}
			}
		}
	}
}
