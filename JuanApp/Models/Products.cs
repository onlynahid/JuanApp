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
        public string Name { get; set; }
        public string? DetailUrl { get; set; }
        public string DetailDescription { get; set; }
        public bool InStock { get; set; }
        public Color? Color { get; set; }
        public int? ColorId { get; set; }
        
    }
}
