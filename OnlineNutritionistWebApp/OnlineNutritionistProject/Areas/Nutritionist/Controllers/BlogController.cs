using AutoMapper.Execution;
using CoreLayer.Models;
using CoreLayer.Services;
using Elfie.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineNutritionistProject.Areas.Member.Models;
using OnlineNutritionistProject.Areas.Nutritionist.Models;
using System.IO;
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
                    Id =user.Id
                };

                
                
                
                var value = await _blogService.GetBlogForNutrition(appUser.Id);
                
                return View(value);
            }
            else
            {

                return NotFound();
            }
        }


        public async Task<IActionResult> DeleteBlog(int id)
        {
            var value = await _blogService.GetByIdAsync(id);
            await _blogService.RemoveAsync(value);
            return RedirectToAction("ListBlog");

        }

        [HttpGet]
        public async Task<IActionResult> AddBlog()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.userId = user.Id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBlog(Blog blog)
        {


            if (blog.ImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(blog.ImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/blogsimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await blog.ImageUrl.CopyToAsync(stream);
                blog.Image = imagename;
            }
            if (blog.CoverImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(blog.CoverImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/blogsimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await blog.CoverImageUrl.CopyToAsync(stream);
                blog.CoverImage = imagename;
            }


            
            blog.CreatedDate = DateTime.Now;

            await _blogService.AddAsync(blog);

            return RedirectToAction("ListBlog");
        }

        [HttpGet]
        public async Task<IActionResult> EditBlog(int id)
        {
            

            var value = await _blogService.GetByIdAsync(id);
            
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> EditBlog(Blog blog)
        {

            if (blog.ImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(blog.ImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/blogsimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await blog.ImageUrl.CopyToAsync(stream);
                blog.Image = imagename;
            }
            if (blog.CoverImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(blog.CoverImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/blogsimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await blog.CoverImageUrl.CopyToAsync(stream);
                blog.CoverImage = imagename;
            }





            blog.CreatedDate = DateTime.Now;

            await _blogService.UpdateAsync(blog);

            return RedirectToAction("ListBlog");
        }
    }
}

