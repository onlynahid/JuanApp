using System.Diagnostics;
using JuanApp.Models;
using JuanApp.Models.Data;
using JuanApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly JuanAppDb _context;

        public HomeController(JuanAppDb context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeVm
            {
                sliders = _context.sliders.ToList(),
                services = _context.services.ToList(),
                products = _context.products
                .Include(col=>col.Color)
                .ToList(),
                productitles = _context.productTitles.FirstOrDefault(),
                productAdvertising = _context.productAdvertisings.ToList(),
                newProductsTitles = _context.newProductstitle.FirstOrDefault(),
                productsnew = _context.productsnew.ToList(),
                blogtitle = _context.blogtitle.FirstOrDefault(),
                blogs = _context.blogs.ToList()
            };
            return View(model);
           
        }

       

        
    }
}
