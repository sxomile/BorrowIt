﻿using RentIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.DataAccess.Repository.IRepository
{
	public interface IItemRepository : IRepository<Item>
	{
		void Update(Item item);
	}
}
