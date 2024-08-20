using CoreLayer.Models;
using CoreLayer.Services;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using NuGet.Packaging;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]

    public class PackageController : Controller
    {
        private readonly IPackageService _service;
        private readonly UserManager<AppUser> _userManager;

        public PackageController(IPackageService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ListPackages()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var appUser = new AppUser
                {
                    Id = user.Id
                };

                var value = await _service.GetPacgateForNutritionist(appUser.Id);
                return View(value);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<IActionResult> AddPackage()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Id = user.Id;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPackage(Package package)
        {

            if (package.ImageUrl != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(package.ImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/packagesimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await package.ImageUrl.CopyToAsync(stream);
                package.Image = imagename;
            }

            await _service.AddAsync(package);
            return RedirectToAction(nameof(ListPackages));
        }

    }
}
