using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models
{
	public class Order
	{
		[Key]
		public int OrderId {  get; set; }
		[ValidateNever]
		public string ItemName {  get; set; }
		[ValidateNever]
		public IdentityUser Lender {  get; set; }
		[ValidateNever]
		[ForeignKey("Lender")]
		public string LenderId {  get; set; }
		//lender-a vucem iz item-a
		[ValidateNever]
		public IdentityUser Borrower { get; set; }
		[ValidateNever]
		[ForeignKey("Borrower")]
		public string BorrowerId { get; set; }

	}
}
