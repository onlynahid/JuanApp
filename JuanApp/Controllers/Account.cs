using System.Threading.Tasks;
using JuanApp.Models;
using JuanApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuanApp.Controllers
{
    public class Account
        (
        UserManager<AppUser>userManager,
        SignInManager<AppUser>signInManager
        
        )
        
        : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVm userRegisterVm)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterVm);
            }
            var user = await userManager.FindByNameAsync(userRegisterVm.Fullname);
            if(user != null)
            {
                ModelState.AddModelError("Fullname","Fullname is already taken");
                return View(userRegisterVm);
            }
            user = new AppUser
            {
                Email = userRegisterVm.Email,
                FullName = userRegisterVm.Fullname

            };
            var result = await userManager.CreateAsync(user,userRegisterVm.Password);
            if (result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                       
                }
                return View(userRegisterVm);    
            }

            return RedirectToAction("Login","Account");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVm userLoginVm)
        {   
            if (!ModelState.IsValid)
            {
                return View(userLoginVm);
            }
            AppUser user = await userManager.FindByEmailAsync(userLoginVm.UsernameorEmail);
            if(user is null)
            
               user = await userManager.FindByNameAsync(userLoginVm.UsernameorEmail);
            if(user is null)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View(userLoginVm);
                    
            }
            var result = await signInManager.PasswordSignInAsync(user, userLoginVm.Password, userLoginVm.RememberMe, false);
           if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(userLoginVm);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
