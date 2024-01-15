using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class GetBooks: BaseEntity
    {

        public int BooksId { get; set; }

        public bool status { get; set; }

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public Books Books { get; set; }
    }
}
