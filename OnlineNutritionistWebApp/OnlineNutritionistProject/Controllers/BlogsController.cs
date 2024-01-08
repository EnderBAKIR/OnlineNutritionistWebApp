using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace OnlineNutritionistProject.Controllers
{
	[AllowAnonymous]
	public class BlogsController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IBlogService _blogService;

        public BlogsController(UserManager<AppUser> userManager, IBlogService blogService)
        {
            _userManager = userManager;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
		{
			return View(await _blogService.GetBlogWithNutrition());
		}

	
		[HttpGet]
		public async Task<IActionResult> BlogsDetails(int id)

		{

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.AppuserId = user.Id;

            ViewBag.BlogId = id;
            var blog = await _blogService.GetBlogAsync();
            return View(blog);

        }
			
	}
}
