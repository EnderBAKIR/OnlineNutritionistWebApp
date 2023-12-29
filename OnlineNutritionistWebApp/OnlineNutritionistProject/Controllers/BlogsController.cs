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
			return View(await _blogService.GetAllAsync());
		}

	
		[HttpGet]
		public async Task<IActionResult> BlogsDetails(int id, AppUser user)

		{
			
			var value = await _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.UserImage = user.ImageUrl;
			ViewBag.AppuserId = value.Id;
			ViewBag.BlogId = id;
			var values = await _blogService.GetByIdAsync(id);
			return View(values);

		}

				
	}
}
