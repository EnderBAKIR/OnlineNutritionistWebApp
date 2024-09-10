using CoreLayer.Models;
using CoreLayer.Services;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using NuGet.Packaging;
using NuGet.Protocol;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]

    public class PackageController : Controller
    {
        private readonly IPackageService _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDonateService _donateService;

        public PackageController(IPackageService service, UserManager<AppUser> userManager, IDonateService donateService)
        {
            _service = service;
            _userManager = userManager;
            _donateService = donateService;
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
        public async Task<IActionResult> AddPackage(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            bool donateExists = _donateService.DoesDonateExistForPackage(user.Id);

            if (donateExists)
            {
                ViewBag.Id = user.Id;
                return View();
            }
            else
            {
                return RedirectToAction("DonateCheck", "Donate");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPackage(Package package)
        {
            var donateuser = await _donateService.GetAllAsync();
            if (donateuser == null)
            {
                return RedirectToAction("DonateCheck", "Donate");
            }
            else
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
            }
            package.CreatedDate = DateTime.Now;
            await _service.AddAsync(package);
            return RedirectToAction(nameof(ListPackages));
        }


        [HttpGet]
        public async Task<IActionResult> EditPackage(int id)
        {
            var values = await _service.GetByIdAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> EditPackage(Package package)
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

            await _service.Update(package);
            return RedirectToAction(nameof(ListPackages));
        }


        public async Task<IActionResult> RemovePackage(int id)
        {
            var values = await _service.GetByIdAsync(id);
            await _service.Remove(values);
            return RedirectToAction(nameof(ListPackages));
        }

        public async Task<IActionResult> HidePackage(int id)
        {
            var package = await _service.GetByIdAsync(id);
            package.Remove = true;
            await _service.Update(package);
            return RedirectToAction(nameof(ListPackages));
        }

        public async Task<IActionResult> PublishPackage(int id)
        {
            var package = await _service.GetByIdAsync(id);
            package.Remove = false;
            await _service.Update(package);
            return RedirectToAction(nameof(ListPackages));
        }

    }
}
