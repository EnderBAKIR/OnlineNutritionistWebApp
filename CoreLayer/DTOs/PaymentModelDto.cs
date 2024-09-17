using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
    public class PaymentModelDto
    {
        public  string CardHolderName { get; set; }
        public  string CardNumber { get; set; }
        public  string ExpireMonth { get; set; }
        public  string ExpireYear { get; set; }
        public  string Cvc { get; set; }
        public  int? RegisterCard { get; set; } = 0;
    }
}
