﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models
{
	public class Country
	{
		public int Id { get; set; }
		public string CountryName {  get; set; }
		public string CountryCode { get; set; }
		public ICollection<City> Cities { get; set; }
	}
}
