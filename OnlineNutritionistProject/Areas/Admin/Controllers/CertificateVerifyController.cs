using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CertificateVerifyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppuserService _appuserService;
        public CertificateVerifyController(UserManager<AppUser> userManager, IAppuserService appuserService)
        {
            _userManager = userManager;
            _appuserService = appuserService;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> VerifyCertificate(int id)
        {
            var user = await _appuserService.GetByIdAsync(id);
            if (user != null && !string.IsNullOrEmpty(user.CertificateImage))
            {
                user.CertificateStatus = CertificateStatus.Approved;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> InvalidateCertificate(int id)
        {
            var user = await _appuserService.GetByIdAsync(id);
            if (user != null)
            {
                user.CertificateImage = null;
                user.CertificateStatus = CertificateStatus.Invalid;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UndoCertificate(int id)
        {
            var user = await _appuserService.GetByIdAsync(id);
            if (user != null && !string.IsNullOrEmpty(user.CertificateImage))
            {
                user.CertificateStatus = CertificateStatus.Pending;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DownloadCertificate(string certificateName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "certificates", certificateName);
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/pdf", certificateName);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
