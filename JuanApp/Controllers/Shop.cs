using JuanApp.Models.Data;
using JuanApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JuanAppDb _context;

        public ShopController(JuanAppDb context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var products = _context.products
                .Include(col => col.Color)
                .ToList();
            return View(products);  
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            try
            {
                var product = await _context.products.FindAsync(productId);
                if (product == null)
                    return Json(new { success = false, message = "Product not found" });

                // Get or create cart session
                var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

                var cartItem = cart.FirstOrDefault(x => x.ProductId == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        ProductId = productId,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = 1,
                        ImageUrl = product.Imageurl
                    });
                }

                HttpContext.Session.Set("Cart", cart);

                return Json(new { success = true, cartCount = cart.Sum(x => x.Quantity) });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error adding to cart" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> QuickView(int productId)
        {
            var product = await _context.products
                .Include(p => p.Color)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
                return NotFound();

            return PartialView("_QuickViewPartial", product);
        }
    }
}
