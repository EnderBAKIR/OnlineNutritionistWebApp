using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
    public class PaymentResponseDto
    {
        public string Status { get; set; }
        public string ConversationId { get; set; }
        public string PaymentId { get; set; }
        public string ErrorMessage { get; set; }
        public long SystemTime { get; set; }
    }
}
