using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

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
        public async Task<IActionResult> ListAssociation()
        {
            return View(await _service.GetAllAsync());
        }


        [HttpGet]
        public IActionResult AddAssociation()
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
            return RedirectToAction(nameof(ListAssociation));
        }


        [HttpGet]
        public async Task<IActionResult> EditAssociation(int id)
        {
            var value = await _service.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> EditAssociation(Association association)
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

            await _service.Update(association);
            return RedirectToAction(nameof(ListAssociation));
        }


        public async Task<IActionResult> RemoveAssociation(int id)
        {
            var value = await _service.GetByIdAsync(id);
            await _service.Remove(value);
            return RedirectToAction(nameof(ListAssociation));
        }

    }
}
