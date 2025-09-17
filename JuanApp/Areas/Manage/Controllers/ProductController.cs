using JuanApp.Models;
using JuanApp.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JuanApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class ProductController
        (JuanAppDb _context)
        
        : Controller
    {
        public IActionResult Index()
        {
            var datas = _context.products
                .Include(col => col.Color)
                .ToList();

            return View(datas);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Products products)
        {
            if (!ModelState.IsValid)
            {
                return View(products);
            }

            var existingProduct = _context.products.Find(products.Id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (_context.products.Any(p => p.Id != products.Id && p.Name.ToLower() == products.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "Bu ad artıq mövcuddur");
                return View(products);
            }

            existingProduct.Name = products.Name;
            existingProduct.Imageurl = products.Imageurl;
            existingProduct.Price = products.Price;
            existingProduct.DiscountPrice = products.DiscountPrice;
            existingProduct.WishListIcon = products.WishListIcon;
            existingProduct.AddtocartIcon = products.AddtocartIcon;
            existingProduct.QuickViewIcon = products.QuickViewIcon;
            existingProduct.DetailUrl = products.DetailUrl;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.products.Remove(product);
            _context.SaveChanges();
            TempData["Message"] = "Product deleted successfully.";
            return RedirectToAction(nameof(Index));
        }



    }
}
