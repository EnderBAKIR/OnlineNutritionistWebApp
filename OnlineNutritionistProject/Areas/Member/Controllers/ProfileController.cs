using CoreLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineNutritionistProject.Areas.Member.Models;

namespace OnlineNutritionistProject.Areas.Member.Controllers
{
    [Area("Member")]
    [AllowAnonymous]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            MemberEditViewModel memberEditViewModel = new MemberEditViewModel
            {
                name = values.Name,
                surname = values.Surname,
                phonenumber = values.PhoneNumber,
                mail = values.Email,
                targetWeight = values.TargetWeight,
                currentWeight = values.CurrentWeight,
                description = values.Description
            };

            var param = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.memberName = param.Name + " " + param.Surname;
            ViewBag.UserImage = param.ImageUrl;
            ViewBag.userName = param.UserName;

            return View(memberEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MemberEditViewModel m)
        {
            var member = await _userManager.FindByNameAsync(User.Identity.Name);
            if (m.Image != null)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(m.Image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/memberimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await m.Image.CopyToAsync(stream);
                member.ImageUrl = imagename;
            }

            member.Name = m.name;
            member.Surname = m.surname;
            member.Description = m.description;
            member.CurrentWeight = m.currentWeight;
            member.TargetWeight = m.targetWeight;
            member.PasswordHash = _userManager.PasswordHasher.HashPassword(member, m.password);

            var result = await _userManager.UpdateAsync(member);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View(m);
        }
    }
}
