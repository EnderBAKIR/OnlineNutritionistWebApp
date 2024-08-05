using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    [AllowAnonymous]
    public class GetConsultancyController : Controller
    {
        
        private readonly IService<GetConsultancy> _service;

        public GetConsultancyController(IService<GetConsultancy> service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddConsultancy(GetConsultancy getConsultancy)
        {
            getConsultancy.CreatedDate = DateTime.Now;
            await _service.AddAsync(getConsultancy);
            return RedirectToAction("Index" , "Nutritionist");
        }
    }
}
