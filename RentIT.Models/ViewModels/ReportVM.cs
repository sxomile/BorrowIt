using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models.ViewModels
{
    public class ReportVM
    {
        public ApplicationUser User { get; set; }
        public Report Report { get; set; }
    }
}
