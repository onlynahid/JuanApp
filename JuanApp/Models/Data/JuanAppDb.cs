using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.Models.Data
{
    public class JuanAppDb :IdentityDbContext<AppUser>
    {
        public JuanAppDb(DbContextOptions options) : base(options)
        {
        }

      
        public DbSet<Slider> sliders { get; set; }
        public DbSet<Services> services { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<ProductTitle> productTitles { get; set; }
        public DbSet<ProductAdvertising> productAdvertisings { get; set; }
        public DbSet<NewProductsTitle> newProductstitle { get; set; }
        public DbSet<ProductsNew> productsnew { get; set; }
        public DbSet<BlogTitle> blogtitle { get; set; }
        public DbSet<Blogs> blogs { get; set; }





    }
}
