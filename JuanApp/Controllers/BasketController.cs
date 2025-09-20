using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddToBasket()
        {
            return PartialView("_BasketPartial", new {});
        }

    }
}
