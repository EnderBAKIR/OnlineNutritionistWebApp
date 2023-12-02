using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Comment:BaseEntity
    {
        
        public string AppUserName { get; set; }
        public string AppUserSurname { get; set; }
        public string AppUserImageUrl { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }//Yorumlar direkt kullanıcının adı soyadı ile otantike olucak , ekstra form yok
        public string CommentContent { get; set; }
        public bool CommentStatus { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }


    }
}
