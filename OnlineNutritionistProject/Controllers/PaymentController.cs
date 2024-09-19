using CoreLayer.Models;
using CoreLayer.Services;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineNutritionistProject.Controllers
{
    public class Order
    {
        public string token { get; set; }
    }

    
    public class PaymentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBasketService _basketService;

        public PaymentController(UserManager<AppUser> userManager, IBasketService basketService)
        {
            _userManager = userManager;
            _basketService = basketService;
        }

        public async Task<IActionResult> PayProduct()
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

            // Ben her siparişte tek bir olacak şekilde ayarladığım için sepette tek ürün var o da siparişin kendisi.
            request.BasketItems = new List<BasketItem>
    {
        new BasketItem
        {
            Id = "1",
            Name = "Paket",
            Category1 = "Paketler",
            ItemType = BasketItemType.VIRTUAL.ToString(),
            Price = price+".00", // Sonuna .00 eklemek şart.
        },
    };
            
            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
            ViewBag.Iyzico = checkoutFormInitialize.CheckoutFormContent;
            Order order = new Order();
            order.token = checkoutFormInitialize.Token; // Form'a ait token'ı kayıt ederek sonradan ödeme kontrolü yaparken kullancağız.

           
            return View();
        }

        [Route("payment/callback/{id}")]
        public ActionResult Callback(string id)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-2KzXaPh6Rdvmp2OJGee0eVtmn5YjSTNh";
            options.SecretKey = "sandbox-fC8lqObE6ItJMOJbwJ3Zts6pJy1RVqua";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            Order order = new Order();

            RetrieveCheckoutFormRequest request = new RetrieveCheckoutFormRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = id;
            request.Token = order.token;

            CheckoutForm checkoutForm = CheckoutForm.Retrieve(request, options);

            if (checkoutForm.PaymentStatus == "SUCCESS")
            {
                ViewBag.status = "Ödeme Başarılı";
                return View();
            }
            else
            {
                ViewBag.status = "Ödeme Başarısız";
                return View();
            }
        }
    }
}

