using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Comment:BaseEntity
    {
        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }//Yorumlar direkt kullanıcının adı soyadı ile otantike olucak , ekstra form yok
        public string CommentContent { get; set; }
        public bool CommentStatus { get; set; }
        public int BlogID { get; set; }
        public Blog Blog { get; set; }


    }
}
