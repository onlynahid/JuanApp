using System.Threading.Tasks;
using JuanApp.Areas.Manage.ViewModels;
using JuanApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class AdminAccountController
        (
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager
        )
        : Controller
    {
        public async Task<IActionResult> CreateAdmin()
        {
            AppUser admin = new AppUser
            {
                UserName = "Nahid045",
                FullName = "Nahid Novruzzade",
                Email = "nahid045@gmail.com"
              };
            var result = await userManager.CreateAsync(admin, "@Nahid045");
            await userManager.AddToRoleAsync(admin, "Admin");

            if (!result.Succeeded)
            {
                return Json(result.Errors); // səhvləri göstər
            }
            await userManager.AddToRoleAsync(admin, "Admin");
            return Json(result);
        
           


        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVm adminLoginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(adminLoginVm);
            }
            var admin = await userManager.FindByNameAsync(adminLoginVm.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("UserName", "UserName or Password is incorrect");
                return View(adminLoginVm);
            }
           if(!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                ModelState.AddModelError("UserName", "UserName or Password is incorrect");
                return View(adminLoginVm);
            }
            var password = await userManager.CheckPasswordAsync(admin, adminLoginVm.Password);
            if (!password)
            {
                ModelState.AddModelError("UserName", "UserName or Password is incorrect");
                return View(adminLoginVm);
            }
            await signInManager.SignInAsync(admin, true);
            return RedirectToAction("Index", "Dashboard");
        }
       


           
            }
        }

