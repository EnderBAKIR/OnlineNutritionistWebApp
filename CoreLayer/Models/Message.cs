using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Message  : BaseEntity
    {
        public string Content { get; set; }
        public bool IsRead { get; set; } = false; // Mesajlar varsayılan olarak okunmamış ayarlandı.


        //Gönderen (AppUser tablosuna referans alındı)
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }


        //Alıcı (AppUser tablosuna referans alındı)
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
    }
}
