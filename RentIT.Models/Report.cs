using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models
{
	public class Report
	{
		public int Id {  get; set; }
		public string? Description {  get; set; }
		public string AdminId {  get; set; }
		public ApplicationUser Admin {  get; set; }
		public string UserId {  get; set; }
		public ApplicationUser User { get; set; }
	}
}
