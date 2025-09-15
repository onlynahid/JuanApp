using JuanApp.Models;
using JuanApp.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers
{
    public class Product
        (JuanAppDb _context)

        : Controller
    {
        public IActionResult Index()
        {
          return View();
        }
 
    }
}
