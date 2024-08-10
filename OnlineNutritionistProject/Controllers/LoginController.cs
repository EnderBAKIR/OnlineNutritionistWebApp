using CoreLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineNutritionistProject.Models;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]

    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel u)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(u.username, u.password, false, true); //"false", sistemde hatırlansın mı, "t" şifre beş defa yanlış girildiği taktirde bloklansın mı?/should "false" be remembered in the system, should "t" be blocked if the password is entered incorrectly five times?.
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Default");
                }
                else
                {
                    return RedirectToAction("SignIn", "Login");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel p)
        {
            AppUser appUser = new AppUser()
            {
                Name = p.Name,
                Surname = p.SurName,
                Email = p.Email,
                UserName = p.UserName,
                Category = p.Category,
            };
            if (p.Password == p.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, p.Password);
                if (result.Succeeded)
                {
                    if (p.Category == "Diyetisyen")
                    {
                        appUser.AppNutriId = appUser.Id;

                        await _userManager.UpdateAsync(appUser);
                    }
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(DefaultController.Index), "Default"); // Çıkış yapınca ana sayfaya yönlendir
        }
    }
}
