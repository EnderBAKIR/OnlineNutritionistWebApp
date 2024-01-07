using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]
    public class BlogController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBlogService _blogService;

        public BlogController(UserManager<AppUser> userManager, IBlogService blogService)
        {
            _userManager = userManager;
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> ListBlog()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                var appUser = new AppUser
                {
                    Id = user.Id
                };

                var value = await _blogService.GetBlogForNutrition(appUser.Id);
                
                return View(value);
            }
            else
            {
                // Kullanıcı bulunamazsa (isteğe bağlı olarak) işleme geç
                return NotFound();
            }
        }

        
        public async Task<IActionResult> DeleteBlog(int id)
        {
          var value =  await _blogService.GetByIdAsync(id);
            await _blogService.RemoveAsync(value);
            return RedirectToAction("ListBlog");

        }
        
        public async Task<IActionResult> AddBlog()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBlog(Blog blog)
        {

            ViewBag.userId =await _userManager.FindByNameAsync(User.Identity.Name);
            blog.CreatedDate= DateTime.Now;
            await _blogService.AddAsync(blog);

            return RedirectToAction("ListBlog");
        }

    }
}
