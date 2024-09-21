using CoreLayer.DTOs;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Options = Iyzipay.Options;


namespace ServiceLayer.Services
{
    public class PaymentService
    {
        public PaymentResponseDto ProcessPayment(PaymentModelDto model)
        {
            Options options = new Options
            {
                ApiKey = "sandbox-2KzXaPh6Rdvmp2OJGee0eVtmn5YjSTNh",
                SecretKey = "sandbox-fC8lqObE6ItJMOJbwJ3Zts6pJy1RVqua",
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };

            CreatePaymentRequest request = new CreatePaymentRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = "1.00",
                PaidPrice = "1.00",
                Currency = Currency.TRY.ToString(),
                Installment = 1,
                BasketId = "5",
                PaymentChannel = PaymentChannel.WEB.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = "http://localhost:5000/Payment/Callback"
            };

            PaymentCard paymentCard = new PaymentCard
            {
                CardHolderName = model.CardHolderName,
                CardNumber = model.CardNumber,
                ExpireMonth = model.ExpireMonth,
                ExpireYear = model.ExpireYear,
                Cvc = model.Cvc,
                RegisterCard = 0
            };

            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer
            {
                Id = "BY789",
                Name = "Ender",
                Surname = "Bakr",
                GsmNumber = "05360000000",
                Email = "enderyx_123@hotmail.com",
                IdentityNumber = "74300864791",
                LastLoginDate = DateTime.Now.ToString(),
                RegistrationDate = DateTime.Now.ToString(),
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey"
            };

            request.Buyer = buyer;

            Payment payment = Payment.Create(request, options);

            return new PaymentResponseDto
            {
                Status = payment.Status,
                ConversationId = payment.ConversationId,
                PaymentId = payment.PaymentId,
                ErrorMessage = payment.ErrorMessage,
                SystemTime = payment.SystemTime
            };
        }
    }

}
