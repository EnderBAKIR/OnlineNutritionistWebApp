using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    public class AssociationController : Controller
    {
        private readonly IAssociationService _service;

        public AssociationController(IAssociationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }
    }
}
