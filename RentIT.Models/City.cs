using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models
{
	public class City
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Country Country { get; set; }
		public int CountryId { get; set; }
		public ICollection<ApplicationUser> CityFromUsers { get; set; }
		public ICollection<ApplicationUser> CityOfResidenceUsers { get; set; }
	}
}
