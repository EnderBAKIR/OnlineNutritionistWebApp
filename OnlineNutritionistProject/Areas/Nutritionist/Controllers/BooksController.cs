using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]
    public class BooksController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBooksService _booksService;
        private readonly IService<GetBooks> _getbooksService;

        public BooksController(UserManager<AppUser> userManager, IBooksService booksService, IService<GetBooks> getbooksService)
        {
            _userManager = userManager;
            _booksService = booksService;
            _getbooksService = getbooksService;
        }

        [HttpGet]
        public async Task<IActionResult> ListBooks()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user != null)
            {
                var appUser = new AppUser
                {
                    Id = user.Id
                };

                var value = await _booksService.GetBookForNutrition(appUser.Id);
                return View(value);
            }
            else
            {
                return NotFound();
            }
        }


        public async Task<IActionResult> DeleteBook(int id)
        {
            var value = await _booksService.GetByIdAsync(id);
            await _booksService.RemoveAsync(value);
            return RedirectToAction("ListBooks");

        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.userId = user.Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Books books)
        {
            if (books.ImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(books.ImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/booksimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await books.ImageUrl.CopyToAsync(stream);
                books.Image = imagename;
            }

            if (books.Pdf != null && books.Pdf.Length > 0)
            {
                var pdfExtension = Path.GetExtension(books.Pdf.FileName);
                var pdfName = Guid.NewGuid().ToString() + pdfExtension;
                var pdfFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "bookspdf", pdfName);
                using (var pdfStream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    await books.Pdf.CopyToAsync(pdfStream);
                }
                books.PdfUrl = pdfName;
            }


            books.CreatedDate = DateTime.Now;

            await _booksService.AddAsync(books);
            return RedirectToAction("ListBooks");
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(int id)
        {
            var value = await _booksService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(Books books)
        {
            if (books.ImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(books.ImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/booksimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await books.ImageUrl.CopyToAsync(stream);
                books.Image = imagename;
            }
            books.CreatedDate = DateTime.Now;
            await _booksService.UpdateAsync(books);
            return RedirectToAction("ListBooks");
        }




    }
}
