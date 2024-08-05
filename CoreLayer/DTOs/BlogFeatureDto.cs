using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
    public class BlogFeatureDto
    {
        public int Id { get; set; }
        public int LikeCount { get; set; }
        public DateTime LikeDate { get; set; }
    }
}
