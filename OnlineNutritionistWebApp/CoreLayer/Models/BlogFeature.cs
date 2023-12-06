using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class BlogFeature
    {
        public int Id { get; set; }
        public int LikeCount { get; set; }//bloglarda öne çıkanları belirlemek için kullanacağımız beğeni sayısını tutan prop
        public DateTime LikeDate { get; set; }//2 günde bir öne çıkanları değiştirmek için like tarihini tutan prop
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
