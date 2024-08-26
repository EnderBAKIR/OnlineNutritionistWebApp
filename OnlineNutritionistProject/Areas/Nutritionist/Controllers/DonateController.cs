using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]
    public class DonateController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDonateService _donateService;

        public DonateController(UserManager<AppUser> userManager, IDonateService donateService)
        {
            _userManager = userManager;
            _donateService = donateService;
        }

        
        [HttpGet]
        public async Task<IActionResult> DonateCheck(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            bool donateExists = _donateService.DoesDonateExistForPackage(user.Id);

            if (donateExists)
            {
                return RedirectToAction("AddPackage", "Package"); //If the user has added a donation receipt, they will be directed to the package adding page.
            }
            else
            {
                return View(); //If the user who wants to add a package does not have a donation, they will be directed to the donation receipt adding page.
            }
        }

    }
}
