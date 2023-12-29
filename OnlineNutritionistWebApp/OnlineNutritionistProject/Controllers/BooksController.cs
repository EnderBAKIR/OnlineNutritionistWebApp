using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class BooksController : Controller
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            
            return View(await _booksService.GetBooksWithNutrition(id));
        }
    }
}
