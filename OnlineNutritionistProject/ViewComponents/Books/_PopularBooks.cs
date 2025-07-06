using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.ViewComponents.Books
{
    public class _PopularBooks : ViewComponent
    {
        private readonly IBooksService _booksService;

        public _PopularBooks(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _booksService.GetBooksWithNutrition();
            var popularBooks = books.Take(3).ToList();
            return View(popularBooks);
        }
    }
} 