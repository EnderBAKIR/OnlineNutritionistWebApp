using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineNutritionistProject.Areas.Nutritionist.Models;

namespace OnlineNutritionistProject.Controllers
{
    public class NutritionistCertificateController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        public NutritionistCertificateController(UserManager<AppUser> userManager)
        {

            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile certificateImage)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (certificateImage != null && certificateImage.Length > 0)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(certificateImage.FileName);
                var imageName = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(resource, "wwwroot", "certificates", imageName);

                using (var stream = new FileStream(saveLocation, FileMode.Create))
                {
                    await certificateImage.CopyToAsync(stream);
                }

                user.CertificateImage = imageName; // Dosya adını sakla
            }

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index", "Default");
        }
    }
}
