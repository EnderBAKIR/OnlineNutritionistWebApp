using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
	public class BlogFeature
	{
		public int Id { get; set; }
		public DateTime LikeDate { get; set; }//2 günde bir öne çıkanları değiştirmek için like tarihini tutan prop//property that holds the like date to change the highlights every two days
        public bool status { get; set; }//beğeni ekleyip çekme//add and remove like
        public int BlogId { get; set; }
		public Blog Blog { get; set; }
		public int AppUserId { get; set; }
		public AppUser AppUser { get; set; }

		
	}
}
