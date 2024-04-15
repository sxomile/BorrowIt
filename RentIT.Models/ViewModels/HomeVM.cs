using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models.ViewModels
{
	public class HomeVM
	{
		public IEnumerable<Item> Items { get; set; }
        public string City { get; set; }
		public string Country {  get; set; }
		public bool Nearby {  get; set; }
    }
}
