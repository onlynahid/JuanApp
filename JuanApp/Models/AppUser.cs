using Microsoft.AspNetCore.Identity;

namespace JuanApp.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public List<BasketItem> basketitems { get; set; }
    }
}
