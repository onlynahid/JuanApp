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
            var books = _context.products.ToList();

            return View(books);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Products products)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (string.IsNullOrWhiteSpace(products.Name))
            {
                return BadRequest();
            }
            if (_context.products.Any(p => p.Name.ToLower() == products.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "This name already exists");
                return View();
            }
            _context.products.Add(products);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Products products = _context.products.FirstOrDefault(p => p.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        [HttpPost]
        public IActionResult Edit(Products products)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Products existproducts = _context.products.FirstOrDefault(p => p.Id == products.Id);
            if (existproducts == null)
            {
                return NotFound();
            }
            if (string.IsNullOrWhiteSpace(products.Name))
            {
                return BadRequest();
            }
            if (_context.products.Any(p => p.Id != products.Id && p.Name.ToLower() == products.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "This name already exists");
                return View();
            }
            existproducts.Name = products.Name;
            existproducts.Imageurl = products.Imageurl;
            existproducts.Price = products.Price;
            existproducts.DiscountPrice = products.DiscountPrice;
            existproducts.WishListIcon = products.WishListIcon;
            existproducts.AddtocartIcon = products.AddtocartIcon;
            existproducts.QuickViewIcon = products.QuickViewIcon;
            existproducts.WishlistUrl = products.WishlistUrl;
            existproducts.AddtoCartUrl = products.AddtoCartUrl;
            existproducts.QuickViewUrl = products.QuickViewUrl;
            existproducts.DetailUrl = products.DetailUrl;
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
