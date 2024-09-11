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
        public async Task<IActionResult> AddDonateReceipt()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var donate = await _donateService.GetByIdAsync(user.Id);
            ViewBag.userId = user.Id;

            var donatesituation = await _donateService.GetDonateForNutritionistAsync(user.Id); //The dietitian's donation receipt status will be displayed. 
            ViewBag.Donate = donatesituation;                                                  //Diyetisyenin bağış dekont durumu gösterilecek.

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddDonateReceipt(Donate donationreceipt)
        {
            if (donationreceipt.DonatePdf != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(donationreceipt.DonatePdf.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/donatespdf/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await donationreceipt.DonatePdf.CopyToAsync(stream);
                donationreceipt.DonatePdfUrl = imagename;
            }

            await _donateService.AddAsync(donationreceipt);
            return RedirectToAction("ListPackages", "Package");
        }

        [HttpGet]
        public async Task<IActionResult> DonateCheck(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            bool donateExists = _donateService.DoesDonateExistForPackage(user.Id);

            if (donateExists)
            {
                return RedirectToAction("AddPackage", "Package");//If the user has added a donation receipt, they will be directed to the package adding page.
            }
            else
            {
                return View();   //If the user who wants to add a package does not have a donation, they will be directed to the donation receipt adding page.
            }
        }

        public IActionResult DownloadReceipt(string receipt)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "donatespdf", receipt);
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/pdf", receipt);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
