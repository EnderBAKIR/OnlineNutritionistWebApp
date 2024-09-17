using CoreLayer.DTOs;
using CoreLayer.Models;
using CoreLayer.Services;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Controllers
{
    
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketService _basketService;

        private readonly string _apiKey = "sandbox-2KzXaPh6Rdvmp2OJGee0eVtmn5YjSTNh";
        private readonly string _secretKey = "sandbox-fC8lqObE6ItJMOJbwJ3Zts6pJy1RVqua";
        private readonly string _baseUrl = "https://sandbox-api.iyzipay.com"; // Test ortamı için sandbox URL

        public PaymentController(PaymentService paymentService, UserManager<AppUser> userManager, IBasketService basketService)
        {
            _paymentService = paymentService;
            _userManager = userManager;
            _basketService = basketService;
        }

        [HttpGet]
        public IActionResult ProcessPayment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentModelDto paymentModelDto)
        {
            // Kullanıcı ve sepet bilgilerini burada getirin (Bunları model olarak alabilirsiniz)
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var ıEnumerableBaskets = await _basketService.GetBasketByAppUserIdAsync(user.Id);
            List<Basket> baskets = ıEnumerableBaskets.ToList();//IEnumarable yi listeye çevirdik.

            Options options = new Options
            {
                ApiKey = _apiKey,
                SecretKey = _secretKey,
                BaseUrl = _baseUrl
            };

            // Ödeme talebi oluşturuluyor
            CreatePaymentRequest request = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = baskets.Sum(x => x.Price).ToString(),
                PaidPrice = baskets.Sum(x => x.Price).ToString(),
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = Guid.NewGuid().ToString(),
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString()
            };
            
            // Kart bilgilerini ekleyin (Kullanıcıdan form ile alınacak)
            PaymentCard paymentCard = new PaymentCard
            {
                CardHolderName = paymentModelDto.CardHolderName, // Kullanıcıdan alınacak
                CardNumber = paymentModelDto.CardNumber, // Kullanıcıdan alınacak
                ExpireMonth = paymentModelDto.ExpireMonth, // Kullanıcıdan alınacak
                ExpireYear = paymentModelDto.ExpireYear, // Kullanıcıdan alınacak
                Cvc = paymentModelDto.Cvc, // Kullanıcıdan alınacak
                RegisterCard = paymentModelDto.RegisterCard
            };
            request.PaymentCard = paymentCard;

            // Kullanıcı bilgilerini ekleyin
            Buyer buyer = new Buyer
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                GsmNumber = "+905350000000", // Kullanıcıdan alınacak
                Email = user.Email,
                IdentityNumber = "74300864791", // Kimlik numarası (gerçek sistemde kullanıcılardan alınır)
                LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationAddress = "Online hizmet", // Online hizmet olduğu için basit bir adres
                Ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString(), // Kullanıcının IP adresi
                City = user.City,
                Country = "Turkey"
            };
            request.Buyer = buyer;

            // Sepet bilgilerini ekleyin
            List<BasketItem> basketItems = new List<BasketItem>();
            foreach (var basket in baskets)
            {
                basketItems.Add(new BasketItem
                {
                    Id = basket.PackageIdentity.ToString(),
                    Name = basket.PackageName,
                    Category1 = "Diyet Paketi",
                    ItemType = BasketItemType.VIRTUAL.ToString(), // Sanal ürün olduğu için "VIRTUAL"
                    Price = basket.Price.ToString()
                });
            }
            request.BasketItems = basketItems;

            // Ödeme işlemi yapılıyor
            Payment payment = Payment.Create(request, options);

            



            if (payment.Status == "success")
            {
                
                // Ödeme başarılıysa
                return RedirectToAction("Success");
            }
            else
            {
                ViewBag.errormessage = payment.ErrorMessage;
                ViewBag.errorcode = payment.ErrorCode;
                // Ödeme başarısızsa
                return RedirectToAction("Failure");
            }
            
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Failure()
        {
            return View();
        }
    }
}
