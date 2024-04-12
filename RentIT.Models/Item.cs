using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentIT.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name:")]
        [MaxLength(50)]
        public string Name { get; set; }
        [DisplayName("Description:")]
        public string? Description { get; set; }
        [DisplayName("How many days do you want to borrow it?")]
        [Range(1, 365)]
        public int RentTime { get; set; }
        public bool IsGift { get; set; } = false;
        public string ImageUrl {  get; set; } = string.Empty;
        [ValidateNever]
        public IdentityUser Creator {  get; set; }
        [ForeignKey("Creator")]
        [ValidateNever]
        public string CreatorId { get; set; }
    }
}
