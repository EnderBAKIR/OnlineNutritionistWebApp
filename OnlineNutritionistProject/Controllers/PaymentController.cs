using CoreLayer.DTOs;
using CoreLayer.Models;
using CoreLayer.Services;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services;

namespace OnlineNutritionistProject.Controllers
{
    public class PaymentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        private readonly IMessageService _messageService;
        private readonly IPackageService _packageService;
        private readonly IAppuserService _appuserService;

        public PaymentController(UserManager<AppUser> userManager, IBasketService basketService, IOrderService orderService, IMessageService messageService, IPackageService packageService, IAppuserService appuserService)
        {
            _userManager = userManager;
            _basketService = basketService;
            _orderService = orderService;
            _messageService = messageService;
            _packageService = packageService;
            _appuserService = appuserService;
        }

        public async Task<IActionResult> PayProduct(string id)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var userBasket = await _basketService.GetBasketByAppUserIdAsync(user.Id);

            decimal price = 0;
            foreach (var item in userBasket)
            {
                price += item.Price;
            }
            price = Convert.ToInt32(price);

            Options options = new Options();
            options.ApiKey = "sandbox-2KzXaPh6Rdvmp2OJGee0eVtmn5YjSTNh";
            options.SecretKey = "sandbox-fC8lqObE6ItJMOJbwJ3Zts6pJy1RVqua";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            request.Price = price.ToString();
            request.PaidPrice = price.ToString();
            request.Currency = Currency.TRY.ToString();
            request.BasketId = Guid.NewGuid().ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "https://localhost:44311/payment/callback/" + request.ConversationId;
            request.Buyer = new Buyer();
            request.Buyer.Id = user.Id.ToString();
            request.Buyer.Name = user.Name;
            request.Buyer.Surname = user.Surname;
            request.Buyer.Email = user.Email;
            request.Buyer.IdentityNumber = "xxxxxxxxxx";
            request.Buyer.RegistrationAddress = "xxxxxxxx";
            request.Buyer.City = "İstanbul";
            request.Buyer.Country = "Türkiye";

            request.BillingAddress = new Address
            {
                ContactName = user.Name + " " + user.Surname,
                City = "İstanbul",
                Country = "Türkiye",
                Description = "xxxxxxxxxxxxx"
            };

            request.BasketItems = new List<BasketItem>
    {
        new BasketItem
        {
            Id = "1",
            Name = "Paket",
            Category1 = "Paketler",
            ItemType = BasketItemType.VIRTUAL.ToString(),
            Price = price+".00",
        },
    };

            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
            ViewBag.Iyzico = checkoutFormInitialize.CheckoutFormContent;
            TempData["checkout_token"] = checkoutFormInitialize.Token;  // Ödeme kontrolü sırasında kullanılmak üzere token'ı saklayıp, işlem sonucunda 'TempData' aracılığıyla yeniden çağırıyoruz.
            TempData["user.id"] = user.Id;

            return View();
        }

        [Route("payment/callback/{id}")]
        public async Task<ActionResult> Callback(string id)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-2KzXaPh6Rdvmp2OJGee0eVtmn5YjSTNh";
            options.SecretKey = "sandbox-fC8lqObE6ItJMOJbwJ3Zts6pJy1RVqua";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = id;
            request.Token = TempData["checkout_token"]?.ToString();

            CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);

            if (checkoutForm.PaymentStatus == "SUCCESS")
            {
                var userId = Convert.ToInt32(TempData["user.id"]);

                // Kullanıcının sepetini al
                var userBasket = await _basketService.GetBasketByAppUserIdAsync(userId);

                // Yeni siparişi oluştur
                var newOrder = new Order
                {
                    CreatedDate = DateTime.Now,
                    AppUserId = userId,
                    TotalAmount = userBasket.Sum(b => b.Price), // Sepetteki tüm ürünlerin toplam fiyatı
                    OrderItems = new List<CoreLayer.Models.OrderItem>()
                };

                // Sepetteki ürünleri siparişe ekle
                foreach (var basketItem in userBasket)
                {
                    var orderItem = new CoreLayer.Models.OrderItem
                    {
                        PackageName = basketItem.PackageName,
                        PackageIdentity = basketItem.PackageIdentity,
                        Price = basketItem.Price
                    };

                    newOrder.OrderItems.Add(orderItem);

                    //Paket satın alan üye için oluşturulan otomatik mesaj sistemi//
                    //Paketi alıp, diyetisyen ID'sine ulaşıyoruz.
                    var package = await _packageService.GetByIdAsync(basketItem.PackageIdentity);
                    await _appuserService.GetAllAsync();
                    if (package != null)
                    {

                        var message = new Message
                        {
                            SenderId = package.AppUserId,
                            Sender = package.AppUser,
                            ReceiverId = userId,
                            Content = $" {package.AppUser.Name + " " + package.AppUser.Surname}: Değerli danışanım, {basketItem.PackageName} paketini satın aldığınız için teşekkür ederim. " +
                                "Size en iyi şekilde hizmet vermek için programınızı en kısa sürede hazırlayacağım. " +
                                "Sorularınız için buradan benimle iletişime geçebilirsiniz. İyi günler dilerim! 😊",
                            IsRead = false,
                            CreatedDate = DateTime.Now
                        };
                        await _messageService.SaveMessageAsync(message);

                    }
                }


                await _orderService.CreateOrderAsync(newOrder);

                ViewBag.status = "Ödeme Başarılı";

            }
            else
            {
                ViewBag.status = "Ödeme Başarısız";

            }
            return View();
        }

    }
}



