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
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		private readonly ApplicationDbContext _db;
		public OrderRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
		public void Update(Order order)
		{
			var orderDB = _db.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
			if (orderDB != null)
			{
				orderDB.IsReturned = order.IsReturned;	//jedina moguca promena u ovom momentu
			}
		}
	}
}
