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
    public class ItemCategoryRepository : Repository<ItemCategory>, IItemCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public ItemCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
