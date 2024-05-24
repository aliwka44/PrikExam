using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Prk1.Models;
using Prk1.ViewModels.Account;

namespace Prk1.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager) : Controller
    {

        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AppUser user = new AppUser
            {
                FullName = vm.FullName,
                Email = vm.Email,
                UserName = vm.UserName,
                
            };

            await _userManager.CreateAsync(user, vm.Password);


            return RedirectToAction("Login", "Account");
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            AppUser? user = await _userManager.FindByNameAsync(vm.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "ERROR");
            }
            if (!ModelState.IsValid) return View(vm);

            var resut = await _signInManager.PasswordSignInAsync(user, vm.Password, true, true);

            if(!resut.Succeeded)
            {
                ModelState.AddModelError("", "ERROR");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
            


        }
    }
}
