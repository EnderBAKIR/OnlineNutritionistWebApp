using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DonationReceiptController : Controller
    {
        private readonly IDonateService _donationService;
        private readonly UserManager<AppUser> _userManager;

        public DonationReceiptController(IDonateService donationService, UserManager<AppUser> userManager)
        {
            _donationService = donationService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _donationService.GetAllAsync());
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

        public async Task<IActionResult> ConfirmReceipt(int id)
        {
            var donate = await _donationService.GetByIdAsync(id);
            if (donate != null)
            {
                donate.DonationStatus = DonationStatus.Approved;
                await _donationService.Update(donate);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RejectReceipt(int id)
        {
            var donate = await _donationService.GetByIdAsync(id);
            if (donate != null)
            {
                donate.DonationStatus = DonationStatus.Invalid;
                await _donationService.Update(donate);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UndotReceipt(int id)
        {
            var donate = await _donationService.GetByIdAsync(id);
            if (donate != null)
            {
                donate.DonationStatus = DonationStatus.NotSubmitted;
                await _donationService.Update(donate);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteReceipt(int id) 
        {
            var donate = await _donationService.GetByIdAsync(id);
            await _donationService.Remove(donate);
            return RedirectToAction(nameof(Index)); 
        }
    }
}
