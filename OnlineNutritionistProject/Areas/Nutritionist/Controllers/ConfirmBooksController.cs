using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineNutritionistProject.ViewComponents.Books;

namespace OnlineNutritionistProject.Areas.Nutritionist.Controllers
{
    [Area("Nutritionist")]
    public class ConfirmBooksController : Controller
    {
        private readonly IGetBooksService _getBooksService;
        private readonly UserManager<AppUser> _userManager;

        public ConfirmBooksController(IGetBooksService getBooksService, UserManager<AppUser> userManager)
        {
            _getBooksService = getBooksService;
            _userManager = userManager;
        }

        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var value = await _getBooksService.RequestListForNutritionist(user.Id);
            
            return View( value);
        }

        
        public  IActionResult Confirm(int id)
        {
           _getBooksService.ChangeToTrue(id);

            return RedirectToAction("ListBooks" , "Books");
        }
    }
}
