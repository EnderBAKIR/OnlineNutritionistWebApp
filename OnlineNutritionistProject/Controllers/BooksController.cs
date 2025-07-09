using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IService<GetBooks> _getBookService;

        public BooksController(IBooksService booksService, UserManager<AppUser> userManager, IService<GetBooks> getBookService)
        {
            _booksService = booksService;
            _userManager = userManager;
            _getBookService = getBookService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int id) //Kitapların listelenmesi // List Books.
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.AppUserId = user?.Id;
            
            var userBookRequests = new List<GetBooks>();
            if (user != null)
            {
                userBookRequests = (await _getBookService.GetAllAsync())
                    .Where(x => x.AppUserId == user.Id)
                    .ToList();
            }
            ViewBag.UserBookRequests = userBookRequests;
            
            return View(await _booksService.GetBooksWithNutrition());
        }


        [HttpPost]
        public async Task<IActionResult> _AddBooksPartial(GetBooks b) //Kullanıcının kitap isteği. //User's book request.
        {
          
            b.CreatedDate = DateTime.Now;
            b.status = false;
            await _getBookService.AddAsync(b);
            return RedirectToAction(nameof(Index), new { success = "true" });
        }
    }
}
