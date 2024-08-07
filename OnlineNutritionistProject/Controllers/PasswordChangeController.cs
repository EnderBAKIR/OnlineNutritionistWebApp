using CoreLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using OnlineNutritionistProject.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;


namespace OnlineNutritionistProject.Controllers
{
	[AllowAnonymous]

	public class PasswordChangeController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public PasswordChangeController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}


		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			string passwordresetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
			var passwordresetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
			{
				userId = user.Id,
				token = passwordresetToken
			}, HttpContext.Request.Scheme);

			MimeMessage mimeMessage = new MimeMessage();

			MailboxAddress mailboxAddressfrom = new MailboxAddress("Admin", "projemail7@gmail.com");

			mimeMessage.From.Add(mailboxAddressfrom);

			MailboxAddress mailboxAddressTo = new MailboxAddress("User", model.Email);

			mimeMessage.To.Add(mailboxAddressTo);

			var bodybuilder = new BodyBuilder();
			bodybuilder.TextBody = passwordresetTokenLink;
			mimeMessage.Body = bodybuilder.ToMessageBody();

			mimeMessage.Subject = "Şifre Değişiklik Talebi";

			SmtpClient client = new SmtpClient();
			client.Connect("smtp.gmail.com", 587, false);
			client.Authenticate("projemail7@gmail.com", "gobi ysyv vzsq ykia");
			client.Send(mimeMessage);
			client.Disconnect(true);

			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string userid, string token)
		{
			TempData["userid"] = userid;
			TempData["token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			var userid = TempData["userid"];
			var token = TempData["token"];
			if (userid == null || token == null)
			{
				return BadRequest();
			}
			var user = await _userManager.FindByIdAsync(userid.ToString());
			var result = await _userManager.ResetPasswordAsync(user, token.ToString(), model.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("SignIn", "Login");
			}
			return View();
		}
	}
}
