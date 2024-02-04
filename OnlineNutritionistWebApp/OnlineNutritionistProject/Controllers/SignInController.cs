using CoreLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineNutritionistProject.Models;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class SignInController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public SignInController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
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
                var result = await _signInManager.PasswordSignInAsync(u.username, u.password, false,true ); //"false", sistemde hatırlansın mı, "t" şifre beş defa yanlış girildiği taktirde bloklansın mı?/should "false" be remembered in the system, should "t" be blocked if the password is entered incorrectly five times?.
                if (result.Succeeded)
                {
                   return RedirectToAction("Index", "Default");
                }
                else
                {
                    return RedirectToAction("SignIn", "SignIn");
                }
            }

            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> LogOut()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Index", "Default");
        //}
    }
}
