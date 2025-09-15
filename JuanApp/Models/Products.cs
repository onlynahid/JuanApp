using System.ComponentModel.DataAnnotations.Schema;

namespace JuanApp.Models
{
    public class Products:BaseEntity
    {
        [NotMapped]
        public IFormFile? formFile { get; set; }
        public string Imageurl { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
        public string? WishListIcon { get; set; }
        public string? AddtocartIcon { get; set; }
        public string? QuickViewIcon { get; set; }
        public string? WishlistUrl { get; set; }
        public string? AddtoCartUrl { get; set; }
        public string? QuickViewUrl { get; set; }
        public string Name { get; set; }
        public string? DetailUrl { get; set; }

    }
}
