using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
    public class PaymentModelDto
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string Cvc { get; set; }
        public int? RegisterCard { get; set; } = 0;
        public decimal Price { get; set; }
        public string BasketId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerEmail { get; set; }
    }
}
