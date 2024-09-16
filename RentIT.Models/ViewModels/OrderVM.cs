using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public Item Item { get; set; }
        public IdentityUser? User { get; set; }
    }
}
