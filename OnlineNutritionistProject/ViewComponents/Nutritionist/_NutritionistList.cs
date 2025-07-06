using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.ViewComponents.Nutritionist
{
    public class _NutritionistList : ViewComponent
    {
        private readonly IAppuserService _appuserService;

        public _NutritionistList(IAppuserService appuserService)
        {
            _appuserService = appuserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var allUsers = await _appuserService.GetAllAsync();
            var nutritionists = allUsers.Where(x => x.Category == "Diyetisyen").Take(3).ToList();
            return View(nutritionists);
        }
    }
} 