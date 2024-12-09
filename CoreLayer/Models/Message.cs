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
        public bool IsRead { get; set; } = false; //Mesajlar varsayılan olarak okunmamış ayarlandı.

        //Mesajlar varsayılan olarak üye ve diyetisyen için silinmiş gözükecek fakat veri tabınında kalacak.
        public bool RemoveMessageForNutri { get; set; } = false;
        public bool RemoveMessageForUser { get; set; } = false;

        //Gönderen (AppUser tablosuna referans alındı)
        public int SenderId { get; set; }
        public AppUser Sender { get; set; }


        //Alıcı (AppUser tablosuna referans alındı)
        public int ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
    }
}
