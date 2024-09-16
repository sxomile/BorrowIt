using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models.ViewModels
{
	public class ItemVM
	{
		public Item Item { get; set; }
		[ValidateNever]
		public IEnumerable<ItemCategory> CategoryList { get; set; }
		[ValidateNever]
        public List<int>? SelectedCategoryIds { get; set; } = new List<int>();
		[ValidateNever]
		public List<string> NewCategoryNames { get; set; } = new List<string>();
	}
}
