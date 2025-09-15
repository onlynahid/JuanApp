using System.Threading.Tasks;
using JuanApp.Models;
using JuanApp.Models.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace JuanApp.Controllers
{
    public class Account
        (
        UserManager<AppUser>userManager,
        SignInManager<AppUser>signInManager,
        RoleManager<IdentityRole> RoleManager

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
                FullName = userRegisterVm.Fullname,
                UserName = userRegisterVm.Fullname

            };
            var result = await userManager.CreateAsync(user,userRegisterVm.Password);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                       
                }
                return View(userRegisterVm);    
            }
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("allupproje@gmail.com"));
            email.To.Add(MailboxAddress.Parse(userRegisterVm.Email));
            email.Subject = "Test Email Subject";
            email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Salam bu confirm emaildir</h1>" };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("allupproje@gmail.com", "pqrf fcbl fmvy kkzf");
            smtp.Send(email);
            smtp.Disconnect(true);
            return RedirectToAction("Login", "Account");
          
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVm userLoginVm)
        {   
            if (!ModelState.IsValid)
            {
                return View(userLoginVm);
            }
            AppUser user = await userManager.FindByNameAsync(userLoginVm.UsernameorEmail);
            if(user is null)
            
               user = await userManager.FindByEmailAsync(userLoginVm.UsernameorEmail);
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
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult MyAccount()
        {
            return View();
        }
        public async Task<IActionResult> CreateRole()
        {
           if(!await RoleManager.RoleExistsAsync("Admin"))
            {
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
           if(!await RoleManager.RoleExistsAsync("Member"))
            {
               await RoleManager.CreateAsync(new IdentityRole("Member"));
            }
           return Json("Roles are created");
        }

    }
}
