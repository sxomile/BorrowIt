using Microsoft.AspNetCore.Identity;
using RentIT.DataAccess.Data;
using RentIT.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.DataAccess.Repository
{
    public class UserRepository : Repository<IdentityUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
    }
}
