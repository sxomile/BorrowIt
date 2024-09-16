namespace RentIT.Models
{
    public class ItemItemCategory
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int ItemCategoryId { get; set; }
        public ItemCategory ItemCategory { get; set; }
    }
}