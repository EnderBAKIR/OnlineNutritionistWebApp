﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int AppUserId { get; set; }
        public string CoverImage { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public BlogFeature BlogFeature { get; set; }
    }
}
