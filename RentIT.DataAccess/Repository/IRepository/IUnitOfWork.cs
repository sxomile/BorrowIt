using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IItemRepository Item {  get; }
		IUserRepository User { get; }
		IOrderRepository Order { get; }
		ICountryRepository Country { get; }
		ICityRepository City { get; }
		IReportRepository Report { get; }
		IItemCategoryRepository ItemCategory { get; }
		IItemItemCategoryRepository ItemItemCategory { get; }
		void Save();
	}
}
