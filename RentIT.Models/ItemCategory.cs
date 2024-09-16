using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentIT.Models
{
    public class ItemCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<ItemItemCategory> ItemItemCategories { get; set; } = new List<ItemItemCategory>();
    }
}
