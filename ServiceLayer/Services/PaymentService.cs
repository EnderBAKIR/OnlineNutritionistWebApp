using CoreLayer.DTOs;
using CoreLayer.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;


namespace ServiceLayer.Services
{
    public class PaymentService
    {
        private readonly string _apiKey = "sandbox-2KzXaPh6Rdvmp2OJGee0eVtmn5YjSTNh";
        private readonly string _secretKey = "sandbox-fC8lqObE6ItJMOJbwJ3Zts6pJy1RVqua";
        private readonly string _baseUrl = "https://sandbox-api.iyzipay.com"; // Test ortamı için sandbox URL

        public PaymentService()
        {
        }

        public Payment MakePayment(AppUser user, List<Basket> baskets)
        {
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
            PaymentModelDto paymentModelDto = new PaymentModelDto();
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
                Ip = "212.125.11.208", // Kullanıcının IP adresi
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

            return payment;
        }
    }
}
