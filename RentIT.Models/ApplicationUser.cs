using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string Name {  get; set; }
		public string? StreetAddress {  get; set; }
		public int CityFromIDId {  get; set; }
		public City CityFromID { get; set; }
		public int CityOfResidenceId {  get; set; }
		public City CityOfResidence { get; set; }
		[Required]
		public string? PhoneNum {  get; set; }
		[JsonIgnore]
		public ICollection<Report> CreatedReports { get; set; }  // Reports created by the Admin
		[JsonIgnore]
		public ICollection<Report> ReceivedReports { get; set; }  // Reports received by the User
	}
}
