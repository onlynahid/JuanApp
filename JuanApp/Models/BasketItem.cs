namespace JuanApp.Models
{
    public class BasketItem:BaseEntity
    {
        public Products products { get; set; }
        public int ProductsId { get; set; }
        public int Count { get; set; }
        public string AppUserId { get; set; }
        public AppUser appUser { get; set; }


    }
}
