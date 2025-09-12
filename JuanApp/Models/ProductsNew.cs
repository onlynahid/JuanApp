namespace JuanApp.Models
{
    public class ProductsNew:BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string DiscountPrice { get; set; }
        public string ImageUrl { get; set; }
        public string DetailUrl { get; set; }
        public string StarIcons { get; set; }
        public string WishListIcon { get; set; }
        public string AddtoCArtIcon { get; set; }
        public string QuickViewIcon { get; set; }
        public string WishListUrl { get; set; }
        public string AddtoCartUrl { get; set; }
        public string QuickViewUrl { get; set; }
    }
}
