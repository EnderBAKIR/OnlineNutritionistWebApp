using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.ViewComponents.Books
{
    public class _LastBooks:ViewComponent
    {
        private readonly IBooksService _booksService;

        public _LastBooks(IBooksService booksService)
        {
            _booksService = booksService;
        }



        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int id)

        {
            var value = await _booksService.LastBooksAsync(id);
            return View(value);

        }
    }
}
