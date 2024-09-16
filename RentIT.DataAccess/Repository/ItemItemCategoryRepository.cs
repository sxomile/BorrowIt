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
    public class ItemItemCategoryRepository : Repository<ItemItemCategory>, IItemItemCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public ItemItemCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
