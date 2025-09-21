using JuanApp.Models;
using JuanApp.Models.Data;
using JuanApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.Controllers
{
    public class BasketController : Controller
    {
        private readonly JuanAppDb _context;
        private readonly ICompositeViewEngine _viewEngine;

        public BasketController(JuanAppDb context, ICompositeViewEngine viewEngine)
        {
            _context = context;
            _viewEngine = viewEngine;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToBasket(int id)
        {
            if (id == 0) 
                return Json(new { success = false, message = "Invalid product" });

            Products product = _context.products
                .Include(c => c.Color)
                .FirstOrDefault(p => p.Id == id);

            if (product == null) 
                return Json(new { success = false, message = "Product not found" });

            var basket = HttpContext.Request.Cookies["basket"];
            List<BasketVm> basketItems = string.IsNullOrWhiteSpace(basket) 
                ? new List<BasketVm>() 
                : System.Text.Json.JsonSerializer.Deserialize<List<BasketVm>>(basket);

            BasketVm basketItem = basketItems.FirstOrDefault(bi => bi.PeoductId == id);

            if (basketItem == null)
            {
                basketItem = new BasketVm
                {
                    PeoductId = id,
                    Count = 1,
                    Name = product.Name,
                    Price = product.Price,
                    Image = product.Imageurl
                };
                basketItems.Add(basketItem);
            }
            else
            {
                basketItem.Count++;
            }

            // Save cookie
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true,
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax
            };

            var serializedBasket = System.Text.Json.JsonSerializer.Serialize(basketItems);
            HttpContext.Response.Cookies.Append("basket", serializedBasket, cookieOptions);

            // Render partial view
            var basketViewModel = new
            {
                ItemCount = basketItems.Sum(x => x.Count),
                Items = basketItems,
                TotalPrice = basketItems.Sum(x => x.Price * x.Count)
            };

            var basketHtml = RenderViewToStringAsync("_BasketPartial", basketViewModel).Result;

            return Json(new
            {
                success = true,
                basketCount = basketViewModel.ItemCount,
                basketHtml = basketHtml,
                message = "Product added to cart successfully"
            });
        }

        private async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(ControllerContext, viewName, false);

                if (viewResult.View == null)
                    throw new ArgumentNullException($"{viewName} does not match any available view");

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
