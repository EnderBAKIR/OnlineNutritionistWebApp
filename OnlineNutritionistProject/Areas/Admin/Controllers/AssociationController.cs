using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AssociationController : Controller
    {
        private readonly IAssociationService _service;

        public AssociationController(IAssociationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> AddAssociation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAssociation(Association association)
        {
            if (association.ImageUrl != null)

            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(association.ImageUrl.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/associationimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await association.ImageUrl.CopyToAsync(stream);
                association.Image = imagename;
            }

            await _service.AddAsync(association);

            return RedirectToAction("ListAssociation");
        }

    }
}
