using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [Area("Member")]
    public class BooksController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGetBooksService _getBooksService;

        public BooksController(UserManager<AppUser> userManager, IGetBooksService booksService)
        {
            _userManager = userManager;
            _getBooksService = booksService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var value = await _getBooksService.StatusListForUser(user.Id);

            return View(value);
            
        }
        public IActionResult DownloadPdf(string pdfName)
        {
            // PDF dosyasının fiziksel yolu
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "bookspdf", pdfName);

            // Dosya var mı kontrolü
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/pdf", pdfName);
            }
            else
            {
                // Dosya bulunamazsa uygun bir hata mesajı gönderilebilir.
                return NotFound();
            }
        }


    }
}
